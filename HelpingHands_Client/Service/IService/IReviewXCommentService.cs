
using HelpingHands_Models;

namespace HelpingHands_Client.Service.IService
{
    public interface IReviewXCommentService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(ReviewXCommentCreateDTO dto);
        Task<T> UpdateAsync<T>(ReviewXCommentUpdateDTO dto);
        Task<T> DeleteAsync<T>(int id);
    }
}
