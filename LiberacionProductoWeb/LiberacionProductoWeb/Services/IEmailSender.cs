using LiberacionProductoWeb.Models;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Services
{
    public interface IEmailSender
    {
        //void SendEmail(Message message);
        Task SendEmailAsync(Message message);
    }
}
