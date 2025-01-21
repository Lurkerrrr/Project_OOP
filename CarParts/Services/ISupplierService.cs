using CarParts.Models;

namespace CarParts.Services
{
    public interface ISupplierService
    {
        List<Supplier> GetAllSuppliers();
        Supplier? GetSupplierById(int id);
        Supplier AddSupplier(Supplier supplier);
        Supplier? UpdateSupplier(int id, Supplier supplier);
        bool DeleteSupplier(int id);
    }
}

