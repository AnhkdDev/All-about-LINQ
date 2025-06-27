namespace Student
{
    //thách thức: ta có danh sách sinh viên
    //list<Student> arr = new List<Student>(){new Student(){Id = "se1", Name = "An"}
    //                                        new Student(){}
    //                                        new Student(){}
    //                                        new Student(){}}; 
    //ta in toàn bộ sinh viên, ta in sinh viên ở tỉnh này, ta in sinh viên có điểm GPA >= 8, GPA >= 8 ở Bình Dương
    //đưa lambda vào arr.Where(s => {s.Id, s.Gpa}
    //tao là hàm where của cái list arr, tao có rất nhiều sinh viên trong tay
    //tao thảy từng sinh viên tên là s cho cái hàm của mày đưa vào. Mày muốn làm gì với s thì mày làm rồi báo tao để tao làm tiếp
    //có 2 cơ chế xử lí: tao list đưa từng đứa, từng object, con số cho hàm ngoài, bên ngoài làm gì thì làm => Action<>: foreach
    //                   tao list đưa từng đứa chúng mày ra hàm ngoài, hàm ngoài báo tao cu đó có hợp lệ không, tao list kiểm soát trở lại: sum, count, where...

    //LINQ:language integrated Query: kĩ thuật truy vấn data trong ram theo 2 style
    //hàm lambda - gốc - method syntax
    //câu sql viết ngược - query syntax
    //merge
    //xài chuỗi... bản chất là trả về object chấm tiếp được 

    //java: stream API, đầu vào của hàm TraiBao() sẽ là 1 object xuất phát từ interface chỉ có duy nhất 1 hàm abstract
                                                                            //functional interface
    //@functional

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}
