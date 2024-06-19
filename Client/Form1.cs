using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Client.Form1;

namespace Client
{
    public partial class Form1 : Form
    {
        HubConnection connection;
        private Elgamal elGamalEncryption = new Elgamal();
        private (BigInteger, BigInteger, BigInteger) publicKey;
        private BigInteger privateKey;
        public Form1()
        {
            InitializeComponent();
            connection = new HubConnectionBuilder().WithUrl("https://localhost:7140/chatHub").Build();
            connection.StartAsync();
        }

        private void btnJoin_Click(object sender, EventArgs e)
        {
            var elGamalEncryption = new Elgamal();

            elGamalEncryption.GenerateKeys();
            publicKey = elGamalEncryption.PublicKey;
            privateKey = elGamalEncryption.PrivateKey;

            txtPublickey_p.Text = publicKey.Item1.ToString();
            txtPublickey_alpha.Text = publicKey.Item2.ToString();
            txtPublickey_beta.Text = publicKey.Item3.ToString();
            txtPrivatekey_a.Text = privateKey.ToString();

            UserConnection userConnection = new UserConnection();

            userConnection.ConnectionID = connection.ConnectionId;
            userConnection.UserName = txtUsername.Text;
            userConnection.PublicKey_p = txtPublickey_p.Text;
            userConnection.PublicKey_alpha = txtPublickey_alpha.Text;
            userConnection.PublicKey_beta = (txtPublickey_beta.Text);
            userConnection.PrivateKey = (txtPrivatekey_a.Text);
            

            connection.InvokeCoreAsync("JoinChat", new object[] { userConnection });
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            if (connection != null)
            {
                connection.On<string, string>("ReceiveMessage", (string user, string message) =>
                {
                    if (lstMessages.InvokeRequired)
                    {
                        lstMessages.Invoke((MethodInvoker)delegate
                        {
                            lstMessages.Items.Add(message);
                        });
                    }
                });
            }
        }


        public class UserConnection
        {
            public string UserName { get; set; }
            public string ConnectionID { get; set; }
            public string PublicKey_p { get; set; }
            public string PublicKey_alpha { get; set; }
            public string PublicKey_beta { get; set; }
            public string PrivateKey { get; set; }
        }
        

        private async void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy khóa công khai của người nhận từ server
                string[] publicKeyStrings = await connection.InvokeCoreAsync<string[]>("GetPublicKey", new object[] { txtReceiver.Text });
                BigInteger publicKey_p = BigInteger.Parse(publicKeyStrings[0]);
                BigInteger publicKey_alpha = BigInteger.Parse(publicKeyStrings[1]);
                BigInteger publicKey_beta = BigInteger.Parse(publicKeyStrings[2]);

                // Chuyển đổi tin nhắn thành BigInteger để mã hóa
                BigInteger plaintextBigInt = new BigInteger(Encoding.UTF8.GetBytes(txtMessage.Text));

                // Mã hóa văn bản bằng khóa công khai của người nhận
                var encryptedData = elGamalEncryption.Encrypt(plaintextBigInt, (publicKey_p, publicKey_alpha, publicKey_beta));

                // Chuyển đổi dữ liệu mã hóa thành chuỗi để truyền đi
                string c1_str = encryptedData.Item1.ToString();
                string c2_str = encryptedData.Item2.ToString();
                Console.WriteLine("Bản mã hóa: " + c1_str + ", " + c2_str);

                // Gửi dữ liệu đã mã hóa và các thông tin khác lên server
                await connection.InvokeCoreAsync("SendMessage", new object[] { txtReceiver.Text, txtUsername.Text, c1_str, c2_str });
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }
        

    }

}
