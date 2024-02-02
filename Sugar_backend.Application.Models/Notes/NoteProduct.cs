using Sugar_backend.Application.Models.Products;

namespace Sugar_backend.Application.Models.Notes;

public class NoteProduct
{
    public Product Product;
    public int Amount;

    public NoteProduct(Product product, int amount)
    {
        Product = product;
        Amount = amount;
    }
}