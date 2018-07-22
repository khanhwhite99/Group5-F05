using System.Collections.Generic;
using Persistence;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class PaymentDAL
    {
       static public List<Payment> GetPaymentByUserId(int id)
        {
           List<Payment> result = new List<Payment>();
            using(MySqlConnection connection = DbConfiguration.OpenConnection())
            {
                string query = $"select * from payment where user_id = {id};";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                using(MySqlDataReader reader = cmd.ExecuteReader())
                {
                    
                    while(reader.Read())
                    {
                        Payment payment = new Payment()
                        {
                            ID = reader.GetInt32("payment_id"),
                            Name = reader.GetString("name"),
                            Money = reader.GetDouble("money")
                        };
                        result.Add(payment);
                    }
                }
            }
            return result;
        }
        static bool CheckPayment(Payment payment, double money)
        {
            if(payment.ID == 1)
                return (payment.Money >= money);
            else
                return false;
        } 
    }
}