using System.Net.WebSockets;

namespace LINQExample
{
    //LINQ (Language Ingegrated Query: Ngôn ngữ truy vấn tích hợp)
    //tương tự SQL
    //Nguồn dữ liệu: IEnumerable, IEnumerable<T> (Aray, List, Stack, Queue ...)
    //               XML, SQL

    public class Product
    {
        public int ID { set; get; }
        public string Name { set; get; }         // tên
        public double Price { set; get; }        // giá
        public string[] Colors { set; get; }     // các màu sắc
        public int Brand { set; get; }           // ID Nhãn hiệu, hãng
        public Product(int id, string name, double price, string[] colors, int brand)
        {
            ID = id; Name = name; Price = price; Colors = colors; Brand = brand;
        }
        // Lấy chuỗi thông tin sản phẳm gồm ID, Name, Price
        override public string ToString()
           => $"{ID,3} {Name,12} {Price,5} {Brand,2} {string.Join(",", Colors)}";

    }
    public class Brand
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var brands = new List<Brand>() {
                new Brand{ID = 1, Name = "Công ty AAA"},
                new Brand{ID = 2, Name = "Công ty BBB"},
                new Brand{ID = 4, Name = "Công ty CCC"},
            };

            var products = new List<Product>()
{
                new Product(1, "Bàn trà",    400, new string[] {"Xám", "Xanh"},         2),
                new Product(2, "Tranh treo", 400, new string[] {"Vàng", "Xanh"},        1),
                new Product(3, "Đèn trùm",   500, new string[] {"Trắng"},               3),
                new Product(4, "Bàn học",    200, new string[] {"Trắng", "Xanh"},       1),
                new Product(5, "Túi da",     300, new string[] {"Đỏ", "Đen", "Vàng"},   2),
                new Product(6, "Giường ngủ", 500, new string[] {"Trắng"},               2),
                new Product(7, "Tủ áo",      600, new string[] {"Trắng"},               3),
            };

            // JOIN - mỗi sản phẩm, in ra tên sản phẩm và tên công ty sản xuất 
            var kqjoin = products.Join(brands, p => p.Brand, b => b.ID, (p, b) =>
            {
                return new
                {
                    Ten = p.Name,
                    Thuonghieu = b.Name
                };
            });
            Console.WriteLine("Phuong thuc join");
            foreach (var product in kqjoin)
            {
                Console.WriteLine(product);
            }

            // GROUPJOIN phần tử trả về là 1 nhóm được nhóm lại theo nguồn ban đầu
            var groupjoin = brands.GroupJoin(products, b => b.ID, p => p.Brand, (b, p) =>
            {
                return new
                {
                   Thuonghieu = b.Name,
                   Sanpham = p
                };
            }); 

            Console.WriteLine("Phuong thuc groupjoin");
            foreach(var product in groupjoin)
            {
                Console.WriteLine(product.Thuonghieu);
                foreach (var item in product.Sanpham)
                {
                    Console.WriteLine(item);
                }
            }

            //TAKE lấy ra 1 số sản phẩm đầu tiên
            Console.WriteLine("Phuong thuc Take");
            products.Take(4).ToList().ForEach(p => Console.WriteLine(p));

            //SKIP bỏ qua các phần tử đầu tiên, lấy các phần còn lại 
            Console.WriteLine("Phuong thuc SKIP");
            products.Skip(2).ToList().ForEach(p => Console.WriteLine(p));

            //ORDERBY sắp xếp tăng dần
            Console.WriteLine("Sap xep tang dan");
            products.OrderBy(p => p.Price).ToList().ForEach(p => Console.WriteLine(p));

            //ORDERBYDESCENDING sắp xếp giảm dần
            Console.WriteLine("Sap xep giam dan");
            products.OrderByDescending(p => p.Price).ToList().ForEach (p => Console.WriteLine(p));

            //REVERSE đảo ngược thứ tự 

            //GROUPBY nhóm những sản phầm có cùng nội dung
            var kq = products.GroupBy(p => p.Price);
            Console.WriteLine("Nhom cac san pham theo gia");
            foreach (var group in kq)
            {
                Console.WriteLine(group.Key);
                foreach (var item in group)
                {
                    Console.WriteLine(item);
                }
            }

            //DISTINCT loại bỏ những phần tử có cùng giá trị, chỉ giữ 1 phần tử mà thôi
            Console.WriteLine("List mau sac");
            products.SelectMany(p => p.Colors).ToList().ForEach(mau => Console.WriteLine(mau));

            Console.WriteLine("List mau sac sau khi dung distinct");
            products.SelectMany(p => p.Colors).Distinct().ToList().ForEach(mau => Console.WriteLine(mau));

            //SINGLE/SINGLEORDEFAULT kiểm tra các phần tử thỏa mãn điều kiện nào đó
            Console.WriteLine("Phuong thuc Single");
            var p = products.Single(p => p.Price == 600);
            Console.WriteLine(p);//nhược điểm của phương thức single là nếu tìm thấy 2 cái có cùng đk hoặc k tìm thấy cái nào có cùng điều kiện thì sẽ gây lỗi

            //muốn khắc phục thì dùng SingleOrDefault nhưng nếu tìm thấy 2 sp thì vẫn phát sinh lỗi, chỉ là nếu k tìm thấy thì sẽ trả về null

            //ANY trả về true nếu thỏa mãn 1 điều kiện gì đó
            Console.WriteLine("Phuong thuc Any");
            var p2 = products.Any(p => p.Price == 600);
            Console.WriteLine(p2);

            //ALL kiểm tra tất cả các phần tử phải thỏa mãn điều kiện
            Console.WriteLine("Phuong thuc All");
            var p3 = products.All(p => p.Price >= 200);
            Console.WriteLine(p3);

            //COUNT đếm các phần tử thỏa mãn điều kiện
            Console.WriteLine("Phuong thuc COUNT");
            var p4 = products.Count(p => p.Price > 200);
            Console.WriteLine(p4);
            
            var p5 = products.Count();
            Console.WriteLine(p5);

            //In ra tên sản phẩm, thương hiệu, lấy các sản phẩm thỏa mãn: giá (300 - 400), giá giảm dần
            Console.WriteLine("Các sp giá (300 - 400), giá giảm dần");
            var p6 = products.Where(p => p.Price >= 300 && p.Price <= 400).OrderByDescending(p => p.Price).Join(brands, p => p.ID, b => b.Name, (p, b) =>
            {
                return new
                {
                    Thuonghieu = b.Name,
                    Sanpham = p
                };
            }).ToList();
            foreach (var item in p6)
            {
                Console.WriteLine(item);
            }

        }
    }
}
