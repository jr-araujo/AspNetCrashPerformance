using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppCrashPerformance.Models;

namespace WebAppCrashPerformance.Controllers
{
    public class ProductCompare : IEqualityComparer<Produto>
    {
        public bool Equals(Produto x, Produto y)
        {
            return x.ProdutoId == y.ProdutoId;
        }

        public int GetHashCode(Produto obj)
        {
            return obj.ProdutoId.GetHashCode();
        }
    }

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var contexto = new EFDBContext())
            {
                var produtos = contexto.Produtos.ToList();

                HttpContext.Cache.Insert("produtos", produtos);

                ConcurrentBag<Produto> bag = new ConcurrentBag<Produto>(ObterProdutosCache() ?? new List<Produto>());

                //var resultado = produtos.AsParallel().Where(p => bag.Any(x => x.Modelo == p.Modelo)).ToList();
                var resultado = produtos.Except(bag, new ProductCompare()).ToList();
            }

            return View();
        }

        private IList<Produto> ObterProdutosCache()
        {
            // O mecanismo de cache retorna os 14K (apenas para simplificar o armazenamento no cache)
            return (IList<Produto>)HttpContext.Cache["produtos"];
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}