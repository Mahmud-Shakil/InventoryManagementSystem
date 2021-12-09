using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Ev.Models.ViewModel
{
    public class CustomerRepository
    {
        MEVEntities dbobj = new MEVEntities();
        public IEnumerable<CustomerViewModel> GetAllCustomer()
        {
            List<CustomerViewModel> Cuslist = dbobj.Customers.Select(e => new CustomerViewModel
            {
                CustomerId = e.CustomerId,
                CustomerName = e.CustomerName,
                Address = e.Address,
                Email = e.Email,
                MobileNo = e.MobileNo,
                OrderDate = e.OrderDate,
                ImageName = e.ImageName,
                ImageUrl = e.ImageUrl,
                CategoryType = e.CustomerCategory.CategoryType,
                CategoryId = e.CategoryId,
                ProductId = e.ProductId,
                Price=e.Price,
                Quantity=e.Quantity,
                ProductName = e.Product.ProductName,
                SupplierName = e.Product.Supplier.SupplierName,
                PageTitle = "Customer List"


            }).ToList();
            return Cuslist;

        }
        public Customer GetCustomer(int id)
        {
            Customer obj = dbobj.Customers.SingleOrDefault(e => e.CustomerId == id);
            return obj;
        }
        public void SaveCustomer(Customer obj)
        {
            dbobj.Customers.Add(obj);
            dbobj.SaveChanges();
        }
        public void UpdateCustomer(Customer Upobj)
        {
            dbobj.Entry(Upobj).State = EntityState.Modified;
            dbobj.SaveChanges();
        }
        public void DeleteCusotmer(int id)
        {
            Customer delCus = GetCustomer(id);
            dbobj.Customers.Remove(delCus);
            dbobj.SaveChanges();
        }
        public List<CustomerCategory> GetCategories()
        {
            List<CustomerCategory> list = dbobj.CustomerCategories.ToList();
            return list;
        }
        public void SaveCategory(CustomerCategory obj)
        {
            dbobj.CustomerCategories.Add(obj);
            dbobj.SaveChanges();
        }
        public List<Supplier> GetSupplier()
        {
            List<Supplier> list = dbobj.Suppliers.ToList();
            return list;
        }
        
        public void SaveSupplier(Supplier obj)
        {
            dbobj.Suppliers.Add(obj);
            dbobj.SaveChanges();
        }
        public List<Product> GetProduct()
        {
            List<Product> list = dbobj.Products.ToList();
            return list;
        }
        public void SaveProduct(Product obj)
        {
            dbobj.Products.Add(obj);
            dbobj.SaveChanges();
        }
       

    }
}