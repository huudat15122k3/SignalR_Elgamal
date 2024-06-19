using Microsoft.AspNetCore.SignalR;
using System.Data;
using System.Data.SqlClient;
using System.Numerics;
using System.Text;

namespace Server.Hubs
{
    public class ChatHub : Hub
    {
        private Elgamal elGamalEncryption = new Elgamal();

        public async Task JoinChat(UserConnection userConnection)
        {
            
            await SaveConnectionID(userConnection);
            await Clients.All.SendAsync("ReceiveMessage", userConnection.UserName, userConnection.UserName + " has join chat");
        }
        public async Task SendMessage(string txtReceiver, string txtSender, string c1_str, string c2_str)
        {
            try
            {
                Console.WriteLine(c1_str + ", " + c2_str);
                // Lấy connectionID của người nhận
                string connectionID = getConnectionIDByReceivername(txtReceiver);

                // Chuyển đổi chuỗi thành các giá trị BigInteger
                BigInteger c1 = BigInteger.Parse(c1_str);
                BigInteger c2 = BigInteger.Parse(c2_str);

                // Lấy khóa riêng tư của người nhận từ cơ sở dữ liệu h
                BigInteger privateKey = GetPrivateKeyByConnectionID(connectionID); 

                // Lấy khóa công khai của người nhận từ cơ sở dữ liệu 
                var publicKey = GetPublicKeyByConnectionID(connectionID);

                // Giải mã dữ liệu
                BigInteger decryptedText = elGamalEncryption.Decrypt((c1, c2), publicKey, privateKey);

                // Chuyển đổi BigInteger đã giải mã thành chuỗi
                string decryptedMessage = Encoding.UTF8.GetString(decryptedText.ToByteArray());

                // Gửi tin nhắn đã giải mã đến người nhận
                await Clients.Client(connectionID).SendAsync("ReceiveMessage", txtSender, txtSender + ": " + decryptedMessage);
            }
            catch (Exception ex)
            {
                // Ghi log lỗi hoặc xử lý ngoại lệ
                throw new HubException($"Error in SendMessage: {ex.Message}");
            }
        }



        string conStr = "Data Source=PC-TINY; Initial Catalog=DTB_Chat; Integrated Security=True;";
        private async Task SaveConnectionID(UserConnection userConnection)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    await con.OpenAsync();
                    using (SqlCommand com = new SqlCommand("deskSaveConnectionUser", con))
                    {
                        com.CommandType = CommandType.StoredProcedure;
                        com.Parameters.AddWithValue("@paraUserName", userConnection.UserName);
                        com.Parameters.AddWithValue("@paraConnectionID", userConnection.ConnectionID);
                        com.Parameters.AddWithValue("@paraPublicKey_p",  userConnection.PublicKey_p);
                        com.Parameters.AddWithValue("@paraPublicKey_alpha" ,userConnection.PublicKey_alpha);
                        com.Parameters.AddWithValue("@paraPublicKey_beta", userConnection.PublicKey_beta);
                        com.Parameters.AddWithValue("@paraPrivateKey_a",  userConnection.PrivateKey);
                        

                        int rowsAffected = await com.ExecuteNonQueryAsync();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Data saved successfully.");
                        }
                        else
                        {
                            Console.WriteLine("No rows affected.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private string getConnectionIDByReceivername(string txtReceiver)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("Select ConnectionID from tableUserConnection where Username = '" + txtReceiver + "'", conStr);
            using (DataTable dt = new DataTable())
            {
                adapter.Fill(dt);
                return dt.Rows.Count > 0 ? dt.Rows[0][0].ToString() : "";
            }
        }

        public async Task<string[]> GetPublicKey(string receiverUsername)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                string query = "SELECT PublicKey_p, PublicKey_alpha, PublicKey_beta FROM tableUserConnection WHERE Username = @Username";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Username", receiverUsername);

                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string publicKey_p = reader["PublicKey_p"].ToString();
                            string publicKey_alpha = reader["PublicKey_alpha"].ToString();
                            string publicKey_beta = reader["PublicKey_beta"].ToString();
                            return new string[] { publicKey_p, publicKey_alpha, publicKey_beta };
                        }
                        else
                        {
                            throw new Exception("No public key found for the given username.");
                        }
                    }
                }
            }
        }
        private (BigInteger, BigInteger, BigInteger) GetPublicKeyByConnectionID(string connectionID)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                string query = "SELECT PublicKey_p, PublicKey_alpha, PublicKey_beta FROM tableUserConnection WHERE ConnectionID = @ConnectionID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ConnectionID", connectionID);

                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            BigInteger publicKey_p = BigInteger.Parse(reader["PublicKey_p"].ToString());
                            BigInteger publicKey_alpha = BigInteger.Parse(reader["PublicKey_alpha"].ToString());
                            BigInteger publicKey_beta = BigInteger.Parse(reader["PublicKey_beta"].ToString());
                            return (publicKey_p, publicKey_alpha, publicKey_beta);
                        }
                        else
                        {
                            throw new Exception("No public key found for the given connection ID.");
                        }
                    }
                }
            }
        }

        private BigInteger GetPrivateKeyByConnectionID(string connectionID)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                string query = "SELECT PrivateKey_a FROM tableUserConnection WHERE ConnectionID = @ConnectionID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ConnectionID", connectionID);

                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return BigInteger.Parse(reader["PrivateKey_a"].ToString());
                        }
                        else
                        {
                            throw new Exception("No private key found for the given connection ID.");
                        }
                    }
                }
            }
        }


    }

    public class UserConnection
    {
        public string UserName { get; set; }
        public string ConnectionID { get; set; }
        public string  PublicKey_p { get; set; }
        public string PublicKey_alpha { get; set; }
        public string PublicKey_beta { get; set; }
        public string PrivateKey { get; set; }
    }

    


}
