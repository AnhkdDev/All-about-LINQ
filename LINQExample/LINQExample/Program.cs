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
            foreach (var product in groupjoin)
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
            products.OrderByDescending(p => p.Price).ToList().ForEach(p => Console.WriteLine(p));

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
            products.Where(p => p.Price >= 300 && p.Price <= 400)
                             .OrderByDescending(p => p.Price)
                             .Join(brands, p => p.Brand, b => b.ID, (p, b) =>
            {
                return new
                {
                    TenSP = p.Name,
                    TenTH = b.Name,
                    Gia = p.Price
                };
            }).ToList().ForEach(info => Console.WriteLine($"{info.TenSP,15} {info.TenTH,15} {info.Gia,15}"));

            //---------------------------------------------------------------------------------------------------------
            //TRUY VẤN LINQ

            /*
            1. Xác định nguồn dữ liệu: from tenphantu in IEnumerables
                   ...where, orderby, ...
            2. Lấy dữ liệu ra: select, group by,...
            */

            //Lấy ra tên các sản phẩm
            Console.WriteLine("In ra ten cac san pham");
            var qr = from a in products
                     select $"{a.Name} : {a.Price}";
            qr.ToList().ForEach(name => Console.WriteLine(name));

            Console.WriteLine("In ra ten cac san pham tra ve phan tu vo danh");
            var qr2 = from a in products
                      select new
                      {
                          Ten = a.Name,
                          Gia = a.Price,
                          AA = "Truong Sa, Hoang Sa la cua VN"
                      };
            qr2.ToList().ForEach(name => Console.WriteLine($"{name.Ten,15} {name.Gia,15} {name.AA,40}"));

            //Lấy ra những sản phẩm có giá bằng 400
            Console.WriteLine("In ra những sản phẩm giá 400");
            var qr3 = from a in products
                      where a.Price == 400
                      select a;
            qr3.ToList().ForEach(price => Console.WriteLine(price));

            //Lấy ra những sản phẩm có giá bằng 400 có màu xanh
            Console.WriteLine("In ra những sản phẩm giá 400");
            var qr4 = from a in products
                      from color in a.Colors
                      where a.Price <= 500 && color == "Xanh"
                      orderby a.Price descending
                      select new
                      {
                          Ten = a.Name,
                          Gia = a.Price,
                          Mau = a.Colors
                      };
            qr4.ToList().ForEach(abc =>
            {
                Console.WriteLine($"{abc.Ten,15} {abc.Gia,15} {string.Join(',', abc.Mau)}");
            });

            //Nhóm sản phẩm theo giá
            Console.WriteLine("Nhom san pham theo gia");
            var qr5 = from c in products
                      group c by c.Price;
            qr5.ToList().ForEach(group =>
            {
                Console.WriteLine(group.Key);//vì đang thực hiện nhóm các sản phẩm theo giá => key ở đây là giá
                group.ToList().ForEach(p => Console.WriteLine(p));
            });

            //Nhóm sản phẩm theo giá, giá sẽ tăng dần
            Console.WriteLine("Nhom san pham theo gia tang dan");
            var qr6 = from c in products
                      group c by c.Price into gr //mỗi group được lưu thành 1 biến tạm gr, ko trả về kq ngay
                      orderby gr.Key//sắp xếp nhóm theo key ( ở đây là giá)
                      select gr;
            ;
            qr6.ToList().ForEach(group =>
            {
                Console.WriteLine(group.Key);//vì đang thực hiện nhóm các sản phẩm theo giá => key ở đây là giá
                group.ToList().ForEach(p => Console.WriteLine(p));
            });

            //truy vấn và trả về các đối tượng:
            //Gia  
            //Cacsanpham
            //Soluong
            Console.WriteLine("Nhom san pham theo gia tang dan tra ve 1 kieu vo danh");
            var qr7 = from c in products
                      group c by c.Price into gr //mỗi group được lưu thành 1 biến tạm gr, ko trả về kq ngay
                      orderby gr.Key//sắp xếp nhóm theo key ( ở đây là giá)
                      let sl = "So luong la: " + gr.Count()//let dùng để tạo ra 1 biến tạm (trung gian trong linq)
                      select new
                      {
                          Gia = gr.Key,
                          Cacsanpham = gr,
                          Soluong = sl

                      };
            ;
            qr7.ToList().ForEach(i =>
            {
                Console.WriteLine(i.Gia);
                Console.WriteLine(i.Soluong);
                i.Cacsanpham.ToList().ForEach(c => Console.WriteLine(c));
            });

            //In ra tên sp, tên công ti, giá sp nhưng chưa in được cái riêng của 2 bảng
            Console.WriteLine("in ra ten sp, ten cong ti, gia sp su dung inner join co ban");
            var qr8 = from product in products
                      join brand in brands on product.Brand equals brand.ID //on là điều kiện để ghép join
                      select new
                      {
                          Ten = product.Name,
                          TenCty = brand.Name,
                          Gia = product.Price
                      };
            qr8.ToList().ForEach(i => Console.WriteLine($"{i.Ten, 15} {i.TenCty, 15} {i.Gia, 15}"));
            //=> tương tự inner join ở SQL, chỉ in ra những gì có chung ở 2 bảng, còn những cái riêng thì không được in ra

            //In ra tên sp, tên công ti, giá sp đã in được cái riêng của 2 bảng
            Console.WriteLine("in ra ten sp, ten cong ti, gia sp ke ca cong ty khong co thuong hieu");
            var qr9 = from product in products
                      join brand in brands on product.Brand equals brand.ID into t //lưu các brand vào 1 biến tạm là t
                      from b in t.DefaultIfEmpty()
                      select new
                      {
                          Ten = product.Name,
                          TenCty = (b != null) ? b.Name : "No Brand",
                          Gia = product.Price
                      };
            qr9.ToList().ForEach(i => Console.WriteLine($"{i.Ten,15} {i.TenCty,15} {i.Gia,15}"));
            //=> tương tự inner join ở SQL, chỉ in ra những gì có chung ở 2 bảng, còn những cái riêng thì không được in ra


        }




    }
}
