using Ev.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ev.Models.ViewModel
{
    public class CustomerViewModel
    {
        [Display(Name = "Customer ID")]
        [Key]
        public int CustomerId { get; set; }
        [Display(Name = "Customer Name")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Name is Required")]
        public string CustomerName { get; set; }
        [Display(Name = "Address")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Address is Required")]
        public string Address { get; set; }
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is Required")]
        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$", ErrorMessage = "Email is not valid")]
        public string Email { get; set; }
        [Display(Name = "Mobile No")]

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Mobile No is Required")]
        public string MobileNo { get; set; }
        [Display(Name = "Order Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [ValidateOrderDate]
        public DateTime OrderDate { get; set; }
        [Display(Name = "Image Name")]
        public string ImageName { get; set; }
        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
        [Display(Name = "Quantity")]
        [Required(ErrorMessage = "Quantity is Required")]
        public int Quantity { get; set; }
        [Display(Name = "Price")]
        [Required(ErrorMessage ="Price is Required")]
        [Range(100,500,ErrorMessage =("Price range must be 100-500"))]
        public decimal Price { get; set; }
        [Display(Name = "Category")]
        [Required(ErrorMessage = "Please Select Category")]
        public int CategoryId { get; set; }
        public string PageTitle { get; set; }
        public string CategoryType { get; set; }
        [Display(Name = "Product")]
        [Required(ErrorMessage = "Please Select Product")]
        public int ProductId { get; set; }

        public string ProductName { get; set; }
        public string SupplierName { get; set; }
    }
}