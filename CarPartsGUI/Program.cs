using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarPartsGUI
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SupplierForm());
            Application.Run(new ProductForm());
            Application.Run(new CategoryForm());
        }

    /*
        TEST DATA (ADD SUPPLIER)
            {
            "Id": 11,
            "Name": "RAuto",
            "ContactInfo": "r@autoparts.com",
            "Address": "232 Aspen Dr",
            "PhoneNumber": "343-345-6789"
            }

        TEST DATA (UPDATE SUPPLIER)
            {
            "Id": 11,
            "Name": "RAuto2",
            "ContactInfo": "r@autoparts.com",
            "Address": "232 Aspen Dr",
            "PhoneNumber": "343-345-6789"
            }
         
        TEST DATA (DELETE SUPPLIER)
            11






        TEST DATA (ADD PRODUCT)
            {
            "id": 5,
            "name": "product",
            "price": 10000,
            "code": "2"
            }

        TEST DATA (UPDATE PRODUCT)
            {
            "id": 5,
            "name": "product2",
            "price": 10000,
            "code": "2"
            }
        
        TEST DATA (DELETE PRODUCT)
            5






        TEST DATA (ADD CATEGORY)
            {
            "Id": 11,
            "Name": "Software",
            "Description": "Software"
            }

        TEST DATA (UPDATE CATEGORY)
            {
            "Id": 11,
            "Name": "Software2",
            "Description": "Software"
            }

        TEST DATA (DELETE CATEGORY)
            11
    */

    }
}
