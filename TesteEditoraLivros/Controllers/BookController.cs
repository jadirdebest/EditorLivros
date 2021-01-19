using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Extensions.Logging;
using TesteEditoraLivros.Application.DTO;
using TesteEditoraLivros.Application.Interface;
using TesteEditoraLivros.Models;

namespace TesteEditoraLivros.Controllers
{
    public partial class BookController : Controller
    {
        private readonly IApplicationServiceBook _serviceBook;

        public BookController(IApplicationServiceBook serviceBook)
        {
            _serviceBook = serviceBook;
        }

        [Authorize(Roles = "Administrator,User")]
        public async Task<IActionResult> Books(BooksModelView model)
        {
            try
            {
                //Aqui eu catpturo qual perfil de acesso que está sendo utilizado, como no claims estou adicionando apenas um perfil,
                //não me preocupei em pegar todos os perfis disponiveis, por isso utilizei o FirstOrDefault
                var Role = User.Claims.Where(p => p.Type == ClaimTypes.Role).FirstOrDefault().Value;
                ViewBag.Role = Role;

                /* Configurei essas condições para atender a demanda de busca para os serviços apropriados
                 * 
                 * Caso não seja setado nenhum valor para a palavra chave e Datas de Busca, irá retornar ao bookList o serviço de Buscar Todos
                 * Caso a palavra chave seja preenchido com algum valor, e altere as datas default, o serviço retornará a Busca por Nome ou Autor com Range de Data
                 * Caso não preencha uma palavra chave, porém seja alterado as datas de publicação, o serviço retornará Busca por Range de Datas
                 * Caso a pessoa digite apenas a palavra chave, sem setar data, o bookList irá ser setado com o serviço de Busca por Nome ou Autor
                 */
                var bookList = string.IsNullOrWhiteSpace(model.KeyWord) && model.InitialDate.Ticks < 10 && model.FinalDate.Ticks < 10 ?
                        await _serviceBook.GetAll() :
                    !string.IsNullOrWhiteSpace(model.KeyWord) && model.InitialDate.Ticks > 10 && model.FinalDate.Ticks > 10 ?
                        await _serviceBook.GetByNameOrAuthorWithRangeDate(model.KeyWord, model.InitialDate, model.FinalDate) :
                    string.IsNullOrWhiteSpace(model.KeyWord) && model.InitialDate.Ticks > 10 && model.FinalDate.Ticks > 10 ?
                        await _serviceBook.GetByRangeDate(model.InitialDate, model.FinalDate) :
                        await _serviceBook.GetByNameOrAuthor(model.KeyWord);

                //A opção de ordenação coloquei usando Linq, pois não irá ser afetado em questão de performance, poderia ter feito todos os filtros usando apenas o Linq
                //só que com esse jeito, eu iria carregar a memória da aplicação com muito "lixo" para ser filtrado sendo que ja poderia filtrar logo no banco de dados
                //como não estou utilizando o entity Framework, o controle das consultas é executado dentro do meu repositório de infra, onde esta montado as query.
                var filtredList = "A".Equals(model.OrderName) ?
                    bookList.OrderBy(o => o.Name) :
                    bookList.OrderByDescending(o => o.Name);

                return View(new BooksModelView(filtredList));
            }
            catch (Exception ex)
            {
                //Normalmente eu coloco uma chamada de Log com tipo de Error pra ser registrado no banco de dados e mostro uma mensagem
                //mais amigável ao usuário, mas ficaria algo assim:
                //Log.Error(ex.message);  Mudaria a ViewBag pra outra mensagem 
                
                ViewBag.Error = ex.Message;
                return View(new BooksModelView(new List<BookDTO>()));
            }
            
        }

        [Authorize(Roles = "Administrator,User")]
        public async Task<IActionResult> BookDetails(int id)
        {
            try
            {
                var book = await _serviceBook.GetById(id);
                return View(book);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }


        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> EditBook(int id)
        {
            try
            {
                var book = await _serviceBook.GetById(id);
                return View(
                    new BookModelView(book.Id, book.Name, book.PublishingCompany, book.Author, book.Resume, book.ISBNCode, book.PublicationDate, book.UrlImage));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return View(new BookModelView());
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public IActionResult EditBook(BookModelView model, string op)
        {
            if (!ModelState.IsValid) return View(model);
            try
            {
                if ("Deletar".Equals(op)) _serviceBook.Remove(new BookDTO() { Id = model.Id });
                if ("Atualizar".Equals(op)) _serviceBook.Update(
                  new BookDTO(model.Id, model.Name, model.PublishingCompany, model.Author, model.Resume,
                  model.ISBNCode, model.PublicationDate, model.UrlImage));
                ViewBag.Url = "/Book/Books";
                return PartialView("_SaveChangesConfirm");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return View();
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult RegisterBook()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterBook(BookModelView model)
        {
            if (!ModelState.IsValid) return View(model);
            try
            {
                _serviceBook.Add(
                new BookDTO(
                    model.Name, model.PublishingCompany, model.Author, model.Resume,
                    model.ISBNCode, model.PublicationDate, model.UrlImage)
                );
                ViewBag.Url = "/Book/RegisterBook";
                return PartialView("_SaveChangesConfirm");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }


            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}