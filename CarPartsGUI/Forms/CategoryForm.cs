using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CarPartsGUI.Models;
using Newtonsoft.Json;

namespace CarPartsGUI
{
    public partial class CategoryForm : Form
    {
        private readonly HttpClient _httpClient;

        public CategoryForm()
        {
            InitializeComponent();
            _httpClient = new HttpClient();

            //note: the port number in the local server URL could change.
            //currently, it is set to https://localhost:7208.
            //you can check the current port by visiting the Swagger UI, which is available when the server is running.
            _httpClient.BaseAddress = new Uri("https://localhost:7208");
        }

        //asynchronous method to fetch all categories from the API.
        private async Task<List<Category>> GetAllCategoriesAsync()
        {
            var response = await _httpClient.GetAsync("/Category");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Category>>(json);
            }
            else
            {
                MessageBox.Show("Error fetching categories.");
                return null;
            }
        }

        //event handler for the 'Load Categories' button click.
        private async void btnLoadCategories_Click(object sender, EventArgs e)
        {
            var categories = await GetAllCategoriesAsync();
            if (categories != null)
            {
                textBox1.Text = string.Join(Environment.NewLine, categories.Select(c => $"{c.Id}: {c.Name}"));
            }
        }

        //event handler for the 'Add Category' button click.
        private async void btnAddCategory_Click(object sender, EventArgs e)
        {
            try
            {
                string inputJson = textBox2.Text;
                var newCategory = JsonConvert.DeserializeObject<Category>(inputJson);

                if (newCategory == null)
                {
                    MessageBox.Show("Invalid JSON format.");
                    return;
                }

                var jsonContent = JsonConvert.SerializeObject(newCategory);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("/Category", content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Category added successfully.");
                    btnLoadCategories_Click(sender, e);
                }
                else
                {
                    MessageBox.Show($"Error adding category: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        //event handler for the 'Update Category' button click.
        private async void btnUpdateCategory_Click(object sender, EventArgs e)
        {
            try
            {
                string inputJson = textBox3.Text;
                var updatedCategory = JsonConvert.DeserializeObject<Category>(inputJson);

                if (updatedCategory == null || updatedCategory.Id <= 0)
                {
                    MessageBox.Show("Invalid JSON format or missing Category ID.");
                    return;
                }

                var jsonContent = JsonConvert.SerializeObject(updatedCategory);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"/Category/{updatedCategory.Id}", content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Category updated successfully.");
                    btnLoadCategories_Click(sender, e);
                }
                else
                {
                    MessageBox.Show($"Error updating category: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        //event handler for the 'Delete Category' button click.
        private async void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox4.Text, out int categoryId))
            {
                var response = await _httpClient.DeleteAsync($"/Category/{categoryId}");
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Category deleted successfully.");
                    btnLoadCategories_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Error deleting category.");
                }
            }
            else
            {
                MessageBox.Show("Invalid Category ID.");
            }
        }

        private void CategoryForm_Load(object sender, EventArgs e)
        {
           
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
