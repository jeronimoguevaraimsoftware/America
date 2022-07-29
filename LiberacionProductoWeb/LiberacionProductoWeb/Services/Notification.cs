using HtmlAgilityPack;
using LiberacionProductoWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Services
{
    public class Notification : INotification
    {
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;
        private readonly IUsersLogin _usersLogin;
        private readonly ILogger<Notification> _logger;
        public Notification(IConfiguration configuration, IEmailSender emailSender, IUsersLogin usersLogin, ILogger<Notification> logger)
        {
            this._configuration = configuration;
            this._emailSender = emailSender;
            this._usersLogin = usersLogin;
            this._logger = logger;
        }

        public async Task NotificationOa(List<Parameter> parameters, List<string> emails, string pathSubject, string pathTemplate, IEnumerable<FileDto> files)
        {
            string subject = await readFileAsync(pathSubject);
            string bodyEmail = await readFileAsync(pathTemplate);
            parameters.ForEach(x =>
            {
                subject = subject.Replace(x.Name, x.Value);
                bodyEmail = bodyEmail.Replace(x.Name, x.Value);
            });
            await sendEmailAsync(emails, subject, bodyEmail, files);
        }

        public string getUriOa(int id)
        {
            string link = $"{this._configuration["urlWebApp"]}/ConditioningOrder/New?IdOP={id}";
            return link;
        }

        private async Task<string> readFileAsync(string path)
        {
            return await System.IO.File.ReadAllTextAsync(path, System.Text.Encoding.UTF8);
        }

        private async Task sendEmailAsync(List<string> to, string subject, string body, IEnumerable<FileDto> files)
        {
            var message = new Message(to, subject, body, files);
            await this._emailSender.SendEmailAsync(message);
        }
        public string getUriOp(int id)
        {
            string link = $"{this._configuration["urlWebApp"]}/ProductionOrder/New?Id={id}";
            return link;
        }

        public async Task SendNotification(List<Parameter> parameters, string plantId, string role, string pathSubject, string pathTemplate)
        {
            string subject = await readFileAsync(pathSubject);
            string bodyEmail = await readFileAsync(pathTemplate);
            parameters.ForEach(x =>
            {
                subject = subject.Replace(x.Name, x.Value);
                bodyEmail = bodyEmail.Replace(x.Name, x.Value);
            });
            var emails = await this._usersLogin.FindByRolePlantIdAsync(plantId, role);
            await sendEmailAsync(emails, subject, bodyEmail, null);
        }

        public async Task NotificationInCompliance(List<Tournumber> tournumbers, List<Parameter> parameters, string plantId, string role, string pathSubject, string pathTemplate)
        {
            string subject = await readFileAsync(pathSubject);
            string bodyEmail = await readFileAsync(pathTemplate);
            parameters.ForEach(x =>
            {
                subject = subject.Replace(x.Name, x.Value);
                bodyEmail = bodyEmail.Replace(x.Name, x.Value);
            });
            var _Doc = new HtmlDocument();
            _Doc.LoadHtml(bodyEmail);
            _Doc.GetElementbyId("tbodyTournumber").InnerHtml = GetTableTournumber(tournumbers);
            var emails = await this._usersLogin.FindByRolePlantIdAsync(plantId, role);
            bodyEmail = _Doc.DocumentNode.OuterHtml;
            await sendEmailAsync(emails, subject, bodyEmail, null);
        }

        private string GetTableTournumber(List<Tournumber> tournumbers)
        {
            var html = string.Empty;
            tournumbers.ForEach(x =>
            {
                html += $"<tr>";
                html += $"<td>{x.Number}</td><td>{x.PipeNumber}</td><td>{(x.IsRelased.HasValue ? x.IsRelased.Value ? "Liberado" : "Rechazado" : "")}</td>";
                html += "</tr>";
            });
            return html;
        }

        public string getUriCheckListTwo(int idOA, string tournumber, string distributionBatch, int checkListId)
        {
            string link = $"{this._configuration["urlWebApp"]}/CheckListQuestions/QuestionsTwo?idOA={idOA}&tournumber={tournumber}&distributionBatch={distributionBatch}&checkListId={checkListId}";
            return link;
        }

        public async Task SendNotification(List<Parameter> parameters, string plantId, string role, string pathSubject, string pathTemplate, List<string> emails)
        {
            string subject = await readFileAsync(pathSubject);
            string bodyEmail = await readFileAsync(pathTemplate);
            StringBuilder Emails = new StringBuilder();
            parameters.ForEach(x =>
            {
                subject = subject.Replace(x.Name, x.Value);
                bodyEmail = bodyEmail.Replace(x.Name, x.Value);
            });
            await sendEmailAsync(emails, subject, bodyEmail, null);
            foreach (var item in emails)
            {
                Emails.Append(item + ",");
            }
            if (Emails.Length > 0)
                _logger.LogInformation("Se mandó un correo con el asunto : " + subject + "  a los siguientes destinatarios : " + Emails.ToString().TrimEnd(','));
        }
        public string getUriCheckListOne(int idOA, int checkListId)
        {
            string link = $"{this._configuration["urlWebApp"]}/CheckListQuestions/QuestionsOne?idOA={idOA}&checkListId={checkListId}";
            return link;
        }
    }
}
