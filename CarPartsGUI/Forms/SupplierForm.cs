using CarPartsGUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CarPartsGUI
{
    public partial class SupplierForm : Form
    {
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Button button6;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private readonly HttpClient _httpClient;

        public SupplierForm()
        {
            InitializeComponent();
            _httpClient = new HttpClient();

            //note: the port number in the local server URL could change.
            //currently, it is set to https://localhost:7208.
            //you can check the current port by visiting the Swagger UI, which is available when the server is running.
            _httpClient.BaseAddress = new Uri("https://localhost:7208");
        }

        //asynchronous method to fetch all suppliers from the API.
        private async Task<List<Supplier>> GetAllSuppliersAsync()
        {
            var response = await _httpClient.GetAsync("/Supplier");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync(); 
                var suppliers = JsonConvert.DeserializeObject<List<Supplier>>(json);
                return suppliers;
            }
            else
            {
                MessageBox.Show("Error");
                return null;
            }
        }

        //event handler for the 'Load Suppliers' button click.
        private async void btnLoadSuppliers_Click(object sender, EventArgs e)
        {
           var suppliers = await GetAllSuppliersAsync();
           if (suppliers != null)
            {
                textBox3.Text = string.Join(Environment.NewLine, suppliers.Select(s => s.Name));
            }
        }

        //event handler for the 'Add Supplier' button click.
        private async void btnAddSupplier_Click(object sender, EventArgs e)
        {
            try
            {
                string inputJson = textBox4.Text;

                var newSupplier = JsonConvert.DeserializeObject<Supplier>(inputJson);

                if (newSupplier == null)
                {
                    MessageBox.Show("Invalid JSON format. Please provide valid supplier data.");
                    return;
                }

                var jsonContent = JsonConvert.SerializeObject(newSupplier);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/Supplier", content);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Supplier added successfully.");
                    btnLoadSuppliers_Click(sender, e); 
                }
                else
                {
                    MessageBox.Show($"Error adding supplier: {response.StatusCode}");
                }
            }
            catch (JsonException)
            {
                MessageBox.Show("Invalid JSON format. Please correct and try again.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        //event handler for the 'Update Supplier' button click.
        private async void btnUpdateSupplier_Click(object sender, EventArgs e)
        {
            try
            {
                string inputJson = textBox5.Text;

                var updatedSupplier = JsonConvert.DeserializeObject<Supplier>(inputJson);

                if (updatedSupplier == null || updatedSupplier.Id <= 0)
                {
                    MessageBox.Show("Invalid JSON format or missing Supplier ID. Please provide valid supplier data.");
                    return;
                }

                var jsonContent = JsonConvert.SerializeObject(updatedSupplier);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"/Supplier/{updatedSupplier.Id}", content);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Supplier updated successfully.");
                    btnLoadSuppliers_Click(sender, e);
                }
                else
                {
                    MessageBox.Show($"Error updating supplier: {response.StatusCode}");
                }
            }
            catch (JsonException)
            {
                MessageBox.Show("Invalid JSON format. Please correct and try again.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        //event handler for the 'Delete Supplier' button click.
        private async void btnDeleteSupplier_Click(object sender, EventArgs e)
        {
            int supplierId;
            if (int.TryParse(textBox6.Text, out supplierId))
            {
                var response = await _httpClient.DeleteAsync($"/Supplier/{supplierId}");
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Supplier deleted successfully.");
                    btnLoadSuppliers_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Error deleting supplier.");
                }
            }
            else
            {
                MessageBox.Show("Invalid Supplier ID.");
            }
        }

        private void InitializeComponent()
        {
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.button6 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(185, 102);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox3.Size = new System.Drawing.Size(297, 116);
            this.textBox3.TabIndex = 0;
            this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(184, 261);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox4.Size = new System.Drawing.Size(298, 39);
            this.textBox4.TabIndex = 1;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(184, 338);
            this.textBox5.Multiline = true;
            this.textBox5.Name = "textBox5";
            this.textBox5.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox5.Size = new System.Drawing.Size(298, 41);
            this.textBox5.TabIndex = 2;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.LightGreen;
            this.button3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button3.Location = new System.Drawing.Point(527, 140);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(148, 43);
            this.button3.TabIndex = 3;
            this.button3.Text = "Get All Suppliers";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.btnLoadSuppliers_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.LightSkyBlue;
            this.button4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button4.Location = new System.Drawing.Point(527, 261);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(148, 39);
            this.button4.TabIndex = 4;
            this.button4.Text = "Add .json Supplier";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.btnAddSupplier_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.LightSkyBlue;
            this.button5.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button5.Location = new System.Drawing.Point(527, 338);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(148, 41);
            this.button5.TabIndex = 5;
            this.button5.Text = "Update .json Supplier";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.btnUpdateSupplier_Click);
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(185, 436);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(94, 22);
            this.textBox6.TabIndex = 6;
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.Crimson;
            this.button6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button6.Location = new System.Drawing.Point(335, 423);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(147, 48);
            this.button6.TabIndex = 7;
            this.button6.Text = "Delete Supplier By ID";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.btnDeleteSupplier_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label1.Location = new System.Drawing.Point(12, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(279, 29);
            this.label1.TabIndex = 8;
            this.label1.Text = "Suppliers Control Panel";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.Location = new System.Drawing.Point(13, 150);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 20);
            this.label2.TabIndex = 9;
            this.label2.Text = "Suppliers:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label3.Location = new System.Drawing.Point(14, 272);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 18);
            this.label3.TabIndex = 10;
            this.label3.Text = "JSON Supplier:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label4.Location = new System.Drawing.Point(14, 350);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 18);
            this.label4.TabIndex = 11;
            this.label4.Text = "JSON Supplier:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label5.Location = new System.Drawing.Point(14, 436);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 18);
            this.label5.TabIndex = 12;
            this.label5.Text = "ID:";
            // 
            // SupplierForm
            // 
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(703, 506);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Name = "SupplierForm";
            this.Text = "Suppliers Control Panel";
            this.Load += new System.EventHandler(this.SupplierForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void SupplierForm_Load(object sender, EventArgs e)
        {

        }
    }
}

