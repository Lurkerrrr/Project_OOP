using CarParts.Helpers;
using CarParts.Models;

namespace CarParts.Services
{
    public class SupplierService : ISupplierService
    {
        private const string FilePath = "Database/suppliers.txt";
        private readonly List<Supplier> _suppliers;

        public SupplierService()
        {
            try
            {
                _suppliers = FileStorageHelper.ReadFromFile<Supplier>(FilePath) ?? new List<Supplier>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading suppliers from file: {ex.Message}");
                _suppliers = new List<Supplier>();
            }
        }

        public List<Supplier> GetAllSuppliers() => _suppliers;

        public Supplier GetSupplierById(int id)
        {
            var supplier = _suppliers.FirstOrDefault(s => s.Id == id);
            if (supplier == null)
                throw new KeyNotFoundException($"Supplier with ID {id} not found.");

            return supplier;
        }

        public Supplier AddSupplier(Supplier supplier)
        {
            if (string.IsNullOrWhiteSpace(supplier.Name))
                throw new ArgumentException("Supplier name is required.");

            supplier.Id = _suppliers.Count > 0 ? _suppliers.Max(s => s.Id) + 1 : 1;
            _suppliers.Add(supplier);

            try
            {
                FileStorageHelper.WriteToFile(FilePath, _suppliers);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing supplier to file: {ex.Message}");
                throw new Exception("Failed to save supplier data.", ex);
            }

            return supplier;
        }

        public Supplier UpdateSupplier(int id, Supplier supplier)
        {
            var existingSupplier = GetSupplierById(id);
            if (existingSupplier == null) return null;

            if (string.IsNullOrWhiteSpace(supplier.Name))
                throw new ArgumentException("Supplier name is required.");

            existingSupplier.Name = supplier.Name;
            existingSupplier.ContactInfo = supplier.ContactInfo;
            existingSupplier.Address = supplier.Address;
            existingSupplier.PhoneNumber = supplier.PhoneNumber;

            try
            {
                FileStorageHelper.WriteToFile(FilePath, _suppliers);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing supplier to file: {ex.Message}");
                throw new Exception("Failed to save supplier data.", ex);
            }

            return existingSupplier;
        }

        public bool DeleteSupplier(int id)
        {
            var removedCount = _suppliers.RemoveAll(s => s.Id == id);
            if (removedCount > 0)
            {
                try
                {
                    FileStorageHelper.WriteToFile(FilePath, _suppliers);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error writing supplier to file: {ex.Message}");
                    throw new Exception("Failed to save supplier data.", ex);
                }
                return true;
            }
            return false;
        }
    }
}
