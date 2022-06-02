using System;
using System.Data.SqlClient;
using System.Collections.Generic;
// "Server=localhost;Database=master;Uid=sa;Pwd=reallyStrongPwd123;" - קישור של ה sql
//SqlConnection mySqlConnection = new SqlConnection("server=localhost\\SQLEXPRESS;database=master;Integrated Security=SSPI;");
namespace sqlccharp
{
    class Program
    {
        static void Main(string[] args)
        {
            int select_option = 0;
            while (true)
            {
                Console.WriteLine(" -- Managing your Workers -- \n");
                Console.WriteLine("1 --> Create a table : "); // גמור
                Console.WriteLine("2 --> Select Some of the table columns : ");// גמור
                Console.WriteLine("3 --> Delet part of table : "); // גמור
                Console.WriteLine("4 --> Add new worker : ");// גמור
                Console.WriteLine("5 --> Find worker Who received a bonus (Enter id worker): ");// גמור
                Console.WriteLine("6 --> Adding a column : "); // גמור
                Console.WriteLine("7 --> Update your table : "); // גמור
                Console.WriteLine("8 --> Enter num 8 quit: \n"); // גמור
               
                Console.Write("Enter your choice : ");
                select_option = int.Parse(Console.ReadLine());

                Console.Clear();

                if (select_option == 1)
                {
                    Console.WriteLine("Excellent let's create a table ! ");
                    Console.Write("Enter name table : ");
                    string c = Console.ReadLine();
                    create_table(c);
                }

                if (select_option == 2)
                {
                    Console.WriteLine("Excellent let's get started ! ");
                    Console.WriteLine("If you want to quit enter 0,0");
                    while (true)
                    {
                        Console.Write("enter number column (5 is max column) : ");
                        int n = int.Parse(Console.ReadLine());
                        Console.Write("enter name table : ");
                        string str = Console.ReadLine().ToLower();
                        if(n == 0 || str == "0")
                        {
                            Console.Write("You left the search !");
                            break;
                        }
                        selectworker(n, str);
                    }
                    Console.WriteLine();
                }
                
                if (select_option == 3)
                {
                    Console.WriteLine("Delete an worker who has reached retirement age : ");
                    Console.Write("Enter number column 1 for id worker / 2 for name worker and id : ");
                    int n = int.Parse(Console.ReadLine());

                    Console.Write("enter name table (Enter workers): ");
                    string str = Console.ReadLine();

                    Workers.selectworkerA(n, str);
                    //////////////////////////////
                    
                    Console.Write("Enetr id worker : ");
                    string id_worker = Console.ReadLine().ToLower();

                    delete_table(id_worker);
                }
                    
                if (select_option == 4)
                {
                    Console.WriteLine("Enter new worker : ");
                    Console.Write("Enter Id worker :");
                    string id = Console.ReadLine();

                    Console.Write("Enter Name worker :");
                    string fname = Console.ReadLine();

                    Console.Write("Enter Last name worker :");
                    string lname = Console.ReadLine();

                    Console.Write("Enter salary worker :");
                    int salary = int.Parse(Console.ReadLine());

                    Console.Write("Enter age worker :");
                    int age = int.Parse(Console.ReadLine());

                    insert_into_table(id, fname, lname, salary,age);
                }   

                if (select_option == 5) // דרך מחלקה
                {
                    Console.WriteLine("Bonus by hour salary and age ! ");
                    Console.Write("Enter id worker : ");
                    string id = Console.ReadLine();
                    Workers.find_data_table(id);
                }
                
                if (select_option == 6)
                {
                    Console.WriteLine("Let's add a column !");
                    Console.Write("Enter name column : ");
                    string add = Console.ReadLine();

                    Console.Write("Enter type of column : ");
                    string type = Console.ReadLine();

                    Console.Write("Enter name table : ");
                    string name_table = Console.ReadLine();

                    alert_table(add, type, name_table);
                }
                   
                if (select_option == 7) // דרך מחלקה
                {
                    Console.WriteLine("Enter id if you want to update one worker salary or enter manual how you want to update : ");
                    string all = Console.ReadLine();
                    if(all.ToLower() == "id")
                    {
                        Console.WriteLine("Update worker salary : ");

                        Console.Write("Enter number column 1 for id worker / 2 for name worker and id : ");
                        int n = int.Parse(Console.ReadLine());

                        Console.Write("enter name table (Enter workers): ");
                        string str = Console.ReadLine();

                        Workers.selectworkerA(n, str); // דרך מחלקה 

                        ////////////////////////////////////////

                        Console.Write("Enter id worker : ");
                        string id = Console.ReadLine();

                        Console.Write("Enter Hourly salary : ");
                        int salary = int.Parse(Console.ReadLine());

                        Workers.update_table(id,salary); // דרך מחלקה
                        
                    }
                    if(all.ToLower() == "manual")
                    {
                        Console.WriteLine("Update workers salaries : ");
                        Console.Write("Enter Hourly salary : ");
                        int salary = int.Parse(Console.ReadLine());

                        Console.Write("Enter where to update : ");
                        int firtcon = int.Parse(Console.ReadLine());

                        Console.Write("Until then update amount : ");
                        int secondcon = int.Parse(Console.ReadLine());

                        Workers.update_tableA(salary, firtcon, secondcon);
                    }
                }
                   
                if (select_option == 8)
                    Console.WriteLine("You chose to quit , Have a good day ! "); break;
            }
        }

        public static void create_table(string name_table) // 1
        {

            try // יצירת טבלה
            {
                string columnsbuffer = "(";
                string inputcolName = "";
                string inputcoltype = "";
                string colLen = "";
                Console.WriteLine("When you want to finish write 0,0");
                while (inputcolName != "0" || inputcoltype != "0")
                {
                    Console.Write("column name : ");
                    inputcolName = Console.ReadLine();
                    Console.WriteLine("you can enter only type - nvarchr , int , float , date ");
                    Console.Write("column type : ");
                    inputcoltype = Console.ReadLine();

                    if(inputcoltype == "nvarchar")
                    {
                        Console.WriteLine("Enter size :");
                        colLen = Console.ReadLine();

                        int test;
                        while(int.TryParse(colLen, out test) == false) // המרה של תו כמחרוזת ולמספר והכנסה שלו כמספר למשתנה טסט
                        {
                            Console.WriteLine("Error!");
                            Console.Write("Enter size :");
                            colLen = Console.ReadLine();

                        }
                        inputcoltype += $"({colLen})";
                    }

                    if (inputcolName != "0" && inputcoltype != "0")
                    {
                        columnsbuffer += $"{inputcolName} {inputcoltype},";

                    }
                }

                columnsbuffer = columnsbuffer.Remove(columnsbuffer.Length - 1, 1);// הורדה של הפסיק בעמודה האחרונה בסוף המחרוזת והוספה של סוגר כדי שלא יהיה שגיעה בסינטקס
                columnsbuffer += ")";

                SqlConnection mySqlConnection = new SqlConnection("Server=localhost;Database=master;Uid=sa;Pwd=reallyStrongPwd123;");
                SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
                mySqlCommand.CommandText = "create table " + name_table + " " + columnsbuffer;
                mySqlConnection.Open();

                int newTable = mySqlCommand.ExecuteNonQuery();
                Console.WriteLine("create table " + name_table + " Successfully ! ");
                mySqlConnection.Close();
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }

        public static void selectworker(int num, string nametable) // 2
        {
            try
            {
                SqlConnection mySqlConnection = new SqlConnection("Server=localhost;Database=master;Uid=sa;Pwd=reallyStrongPwd123;");
                SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
                mySqlCommand.CommandText = "select * from " + nametable;
                mySqlConnection.Open();
                SqlDataReader mySqlDateReader = mySqlCommand.ExecuteReader();

                Console.WriteLine();
                Console.WriteLine("ID_name\t\tF_name\tL_name\thouer$\tAge\n");
                while (mySqlDateReader.Read())
                {
                    if (num == 1)
                        Console.WriteLine("{0}", mySqlDateReader[0].ToString());
                    else
                        if (num == 2)
                        Console.WriteLine("{0}\t{1}", mySqlDateReader[0].ToString(), mySqlDateReader[1].ToString());
                    else
                        if (num == 3)
                        Console.WriteLine("{0}\t{1}\t{2}", mySqlDateReader[0].ToString(), mySqlDateReader[1].ToString(), mySqlDateReader[2].ToString());
                    else
                        if (num == 4)
                        Console.WriteLine("{0}\t{1}\t{2}\t{3}", mySqlDateReader[0].ToString(), mySqlDateReader[1].ToString(), mySqlDateReader[2].ToString(), mySqlDateReader[3].ToString());
                    else
                        Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", mySqlDateReader[0].ToString(), mySqlDateReader[1].ToString(), mySqlDateReader[2].ToString(), mySqlDateReader[3].ToString(), mySqlDateReader[4].ToString());
                }
                Console.WriteLine();
                mySqlDateReader.Close();
                mySqlConnection.Close();
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }

        public static void delete_table(string id ) // 3
        {
            try //מחיקת טבלה
            {
                SqlConnection mySqlConnection = new SqlConnection("Server=localhost;Database=master;Uid=sa;Pwd=reallyStrongPwd123;");
                SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
                mySqlCommand.CommandText = $"DELETE from workers WHERE id_name = '{id}'";
                mySqlConnection.Open();
                
                int n = mySqlCommand.ExecuteNonQuery();
                Console.WriteLine("Delete " + n + " worker");
                mySqlConnection.Close();

            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }

        public static void insert_into_table(string id_name, string firs_name,string last_name,int hour_salary, int aeg) // 4
        {
            try // הוספת נתונים לטבלה
            {
                SqlConnection mySqlConnection = new SqlConnection("Server=localhost;Database=master;Uid=sa;Pwd=reallyStrongPwd123;");
                SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
                mySqlConnection.Open();
                mySqlCommand.CommandText = $"insert into workers values('{id_name}','{firs_name}','{last_name}',{hour_salary},{aeg});";

                int n = mySqlCommand.ExecuteNonQuery();
                Console.WriteLine("insert " + n + " worker");
                mySqlConnection.Close();
               
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }

        public static void alert_table(string name_col, string type, string name_tble) // 6
        {
            try // הוספת עמודה חדשה
            {
                
                SqlConnection mySqlConnection = new SqlConnection("Server=localhost;Database=master;Uid=sa;Pwd=reallyStrongPwd123;");
                SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
                mySqlCommand.CommandText = $"ALTER TABLE {name_tble} ADD {name_col} {type}; ";
                mySqlConnection.Open();
               
                int n = mySqlCommand.ExecuteNonQuery();
                Console.WriteLine("ALTER " + n + " column");
                mySqlConnection.Close();
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }
    }
}
   

