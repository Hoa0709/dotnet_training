using app.Connects;
using app.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using MimeKit.Text;

//using BitmapNet;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
//using System.Net.Mail;

// namespace app.Repository
// {
public interface IEmailService
    {
        Task SendAsync(string userId,int id,string to = null);
    }

    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> userManager;
        private readonly AppDbContext _context;

        public EmailService(UserManager<AppUser> userManager, IConfiguration configuration, AppDbContext context)
        {
            this.userManager = userManager;
            _configuration = configuration;
            _context = context;
        }

        private static byte[] bitmaptoArray(Bitmap bitmapimage)
        {
            using (MemoryStream mstream = new MemoryStream())
            {
 
                bitmapimage.Save(mstream, ImageFormat.Png);
                return mstream.ToArray();
            }
 
        }
        private Stream CreateQRCode(string text){
            QRCodeGenerator QrGenerator = new QRCodeGenerator();
            QRCodeData QrCodeInfo = QrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            QRCode qr = new QRCode(QrCodeInfo);
            Bitmap QRbitmap = qr.GetGraphic(50);
            byte[] bitmapArray = bitmaptoArray(QRbitmap);
            MemoryStream ms = new MemoryStream(bitmapArray, 0, bitmapArray.Length);
            //var Qrcodeimage= string.Format("data:image/png;base64,{0}", Convert.ToBase64String(bitmapArray));
            return ms;
        }
        public async Task SendAsync(string userId,int id, string to = null)//string to, string subject, string html, string from = null)
        {
            var bookticket = await _context.bookTickets
                                .Where(x => x.Id == id && x.UserId == userId)
                                .FirstOrDefaultAsync();
            if(bookticket == null) throw new Exception("Ticket not found");
            var user = await userManager.FindByIdAsync(userId);
            if(user == null) throw new Exception("User not found");
            string from = null; 
            string subject = "Send QRCode";
            // create message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from ?? _configuration["EmailConfiguration:From"]));
            email.To.Add(MailboxAddress.Parse(to ?? user.Email));
            email.Subject = subject;
            var builder = new BodyBuilder ();

            builder.HtmlBody = string.Format (@"<p>Hey {0},<br>
                <p>Send QRcode your ticket and your ticket infomation<br>
                <p>Id Ticket: {1}<br>
                <p>Code Ticket: {2}<br>
                <p>Create at: {3}<br>
                <p>-- Joey<br>",bookticket.FullName,bookticket.Id,bookticket.Code,bookticket.CreateAt);
            builder.Attachments.Add("QRcode.png",CreateQRCode(bookticket.Code));
            email.Body = builder.ToMessageBody ();
            // // send email
            using var smtp = new SmtpClient();
            smtp.Connect(_configuration["EmailConfiguration:SmtpServer"], Convert.ToInt32(_configuration["EmailConfiguration:Port"]), SecureSocketOptions.StartTls);
            smtp.Authenticate(_configuration["EmailConfiguration:Username"], _configuration["EmailConfiguration:Password"]);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
// }