using System;
using System.Net;
using System.Web.Mvc;
using DayZTradeCenter.DomainModel;
using rg.GenericRepository.Core;

namespace DayZTradeCenter.UI.Web.Controllers
{
    public class ItemsController : Controller
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemsController"/> class.
        /// </summary>
        /// <param name="itemsRepository">The items repository.</param>
        /// <exception cref="System.ArgumentNullException">itemsRepository</exception>
        public ItemsController(IRepository<Item> itemsRepository)
        {
            if (itemsRepository == null)
            {
                throw new ArgumentNullException("itemsRepository");
            }

            _itemsRepository = itemsRepository;
        }

        // GET: Items
        public ActionResult Index()
        {
            var model = _itemsRepository.GetAll();

            return View(model);
        }
        
        // GET: Items/Details/5
        public ActionResult Details(int id)
        {
            return FindItem(id);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id, Name")] Item item)
        {
            if (!ModelState.IsValid)
            {
                return View(item);
            }

            _itemsRepository.Insert(item);
            _itemsRepository.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int id)
        {
            return FindItem(id);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, Name")] Item item)
        {
            if (!ModelState.IsValid)
            {
                return View(item);
            }

            _itemsRepository.Update(item);
            _itemsRepository.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = _itemsRepository.GetSingle(id.Value);

            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var model = _itemsRepository.GetSingle(id);

            _itemsRepository.Delete(model);
            _itemsRepository.SaveChanges();

            return RedirectToAction("Index");
        }

        private ActionResult FindItem(int id)
        {
            var model = _itemsRepository.GetSingle(id);

            return model == null
                ? View("NotFound")
                : View(model);
        }

        private readonly IRepository<Item> _itemsRepository;
    }
}