using LiberacionProductoWeb.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Services
{
    public interface INotification
    {
        Task SendNotification(List<Parameter> parameters, string plantId, string role, string pathSubject, string pathTemplate);
        Task NotificationOa(List<Parameter> parameters, List<string> emails, string pathSubject, string pathTemplate, IEnumerable<FileDto> files);
        Task NotificationInCompliance(List<Tournumber> tournumbers, List<Parameter> parameters, string plantId, string role, string pathSubject, string pathTemplate);
        string getUriOa(int id);
        string getUriOp(int id);
        string getUriCheckListTwo(int idOA, string tournumber, string distributionBatch, int checkListId);
        Task SendNotification(List<Parameter> parameters, string plantId, string role, string pathSubject, string pathTemplate, List<string> emails);
        string getUriCheckListOne(int idOA, int checkListId);
    }
}
