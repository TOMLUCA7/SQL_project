using System;
using System.Data.SqlClient;
namespace sqlccharp
{
    public class Workers 
    {
       public static void find_data_table(string id_name)
       {
            try // לבחור נתון ספציפי
            {
                SqlConnection mySqlConnection = new SqlConnection("Server=localhost;Database=master;Uid=sa;Pwd=reallyStrongPwd123;");
                SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
                mySqlCommand.CommandText = "Select * from workers where id_name ='" + id_name + "';";
                mySqlConnection.Open();
                SqlDataReader mySqlDateReader = mySqlCommand.ExecuteReader();

                while (mySqlDateReader.Read())
                {
                    for (int i = 0; i < mySqlDateReader.FieldCount; i++)
                    {
                        if (mySqlDateReader.GetName(i) == "first_name")
                        {
                            Console.WriteLine($"{mySqlDateReader[i]}'s Bonus is {int.Parse(mySqlDateReader["hour_salary"].ToString()) * int.Parse(mySqlDateReader["aeg"].ToString())}");
                        }

                    }

                    Console.WriteLine();
                }

                mySqlDateReader.Close();
                mySqlConnection.Close();
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
       }

        public static void update_table(string id, int salary) 
        {
            try // עדכון של הטבלה לפי תז של עובד
            {
                SqlConnection mySqlConnection = new SqlConnection("Server=localhost;Database=master;Uid=sa;Pwd=reallyStrongPwd123;");
                SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
                mySqlCommand.CommandText = $"UPDATE workers set hour_salary = {salary} WHERE id_name = {id}";
                mySqlConnection.Open();
                
                int n = mySqlCommand.ExecuteNonQuery();
                Console.WriteLine("UPDATE " + n + " worker");
                mySqlConnection.Close();
                mySqlConnection.Close();

            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }

        public static void update_tableA(int salary, int firtcon, int secondcon) 
        {
            try // עדכון שך הטבלה לפי תנאים
            {
                SqlConnection mySqlConnection = new SqlConnection("Server=localhost;Database=master;Uid=sa;Pwd=reallyStrongPwd123;");
                SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
                mySqlCommand.CommandText = $"UPDATE workers set hour_salary = {salary} WHERE hour_salary > {firtcon} AND hour_salary < {secondcon}";
                mySqlConnection.Open();
               
                int n = mySqlCommand.ExecuteNonQuery();
                Console.WriteLine("UPDATE " + n + " worker");
                mySqlConnection.Close();
                mySqlConnection.Close();

            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }

        public static void selectworkerA(int num, string nametable) // לבחור עמודה של תז או שם כדי לדעת למי לעדכן את המשכורת
        {
            try
            {
                SqlConnection mySqlConnection = new SqlConnection("Server=localhost;Database=master;Uid=sa;Pwd=reallyStrongPwd123;");
                SqlCommand mySqlCommand = mySqlConnection.CreateCommand();
                mySqlCommand.CommandText = "select * from " + nametable;
                mySqlConnection.Open();
                SqlDataReader mySqlDateReader = mySqlCommand.ExecuteReader();

                Console.WriteLine();
                Console.WriteLine("ID_name\t\tF_name\n");
                while (mySqlDateReader.Read())
                {
                    if (num == 1)
                        Console.WriteLine("{0}", mySqlDateReader[0].ToString());
                    else
                        Console.WriteLine("{0}\t{1}", mySqlDateReader[0].ToString(), mySqlDateReader[1].ToString());
                   
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

    }
}
