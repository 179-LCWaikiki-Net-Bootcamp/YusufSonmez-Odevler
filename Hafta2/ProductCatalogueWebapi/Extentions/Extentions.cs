using System;

namespace ProductCatalogueWebapi.Extentions
{
    public static class Extentions
    {
        // Ürün fiyat indirimi için yazılmış custom extention. Ürün fiyatına göre indirim yapar
        public static double Discount(this double price)
        {
            if(price > 60.00)
            {
                price -= 20.00;
                return price;
            }
            else
            {
                price -= 10.00;                
                return price;
            }
        }
    }
}