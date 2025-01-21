using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CarPartsGUI.Models;
using Newtonsoft.Json;

namespace CarPartsGUI
{
    public partial class ProductForm : Form
    {
        private readonly HttpClient _httpClient;

        public ProductForm()
        {
            InitializeComponent();
            _httpClient = new HttpClient();

            //note: the port number in the local server URL could change.
            //currently, it is set to https://localhost:7208.
            //you can check the current port by visiting the Swagger UI, which is available when the server is running.
            _httpClient.BaseAddress = new Uri("https://localhost:7208");
        }

        //asynchronous method to fetch all products from the API.
        private async Task<List<Product>> GetAllProductsAsync()
        {
            var response = await _httpClient.GetAsync("/Product");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Product>>(json);
            }
            else
            {
                MessageBox.Show("Error fetching products.");
                return null;
            }
        }

        //event handler for the 'Load Products' button click.
        private async void btnLoadProducts_Click(object sender, EventArgs e)
        {
            var products = await GetAllProductsAsync();
            if (products != null)
            {
                textBox1.Text = string.Join(Environment.NewLine, products.Select(p => $"{p.Id}: {p.Name}"));
            }
        }

        //event handler for the 'Add Product' button click.
        private async void btnAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                string inputJson = textBox2.Text;
                var newProduct = JsonConvert.DeserializeObject<Product>(inputJson);

                if (newProduct == null)
                {
                    MessageBox.Show("Invalid JSON format.");
                    return;
                }

                var jsonContent = JsonConvert.SerializeObject(newProduct);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("/Product", content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Product added successfully.");
                    btnLoadProducts_Click(sender, e);
                }
                else
                {
                    MessageBox.Show($"Error adding product: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        //event handler for the 'Update Product' button click.
        private async void btnUpdateProduct_Click(object sender, EventArgs e)
        {
            try
            {
                string inputJson = textBox3.Text;
                var updatedProduct = JsonConvert.DeserializeObject<Product>(inputJson);

                if (updatedProduct == null || updatedProduct.Id <= 0)
                {
                    MessageBox.Show("Invalid JSON format or missing Product ID.");
                    return;
                }

                var jsonContent = JsonConvert.SerializeObject(updatedProduct);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"/Product/{updatedProduct.Id}", content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Product updated successfully.");
                    btnLoadProducts_Click(sender, e);
                }
                else
                {
                    MessageBox.Show($"Error updating product: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        //event handler for the 'Delete Product' button click.
        private async void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox4.Text, out int productId))
            {
                var response = await _httpClient.DeleteAsync($"/Product/{productId}");
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Product deleted successfully.");
                    btnLoadProducts_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Error deleting product.");
                }
            }
            else
            {
                MessageBox.Show("Invalid Product ID.");
            }
        }

        private void ProductForm_Load(object sender, EventArgs e)
        {
     
        }
    }
}
