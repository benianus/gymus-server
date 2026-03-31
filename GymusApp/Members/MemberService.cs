using gymus_server.Shared.Abstractions;

namespace gymus_server.GymusApp.Members;

public class MemberService(
    ICrudRepository<Member, int> memberRepository
) : ICrudService<Member, int>
{
    public async Task<List<Member>> GetAll()
    {
        return await memberRepository.GetAll();
    }

    public async Task<Member?> GetOne(int id)
    {
        return await memberRepository.GetOne(id);
    }

    public async Task<Member?> Create(Member data)
    {
        return HasInvalidData(data) ? null : await memberRepository.Create(data);
    }

    public async Task<Member?> Update(Member data, int id)
    {
        return HasInvalidData(data) ? null : await memberRepository.Update(data, id);
    }

    public async Task<bool> Delete(int id)
    {
        return await memberRepository.Delete(id);
    }

    private static bool HasInvalidData(Member data)
    {
        return string.IsNullOrWhiteSpace(data.BirthCertificate) ||
               string.IsNullOrWhiteSpace(data.MedicalCertificate) ||
               string.IsNullOrWhiteSpace(data.PersonalPhoto) ||
               data.PersonId <= 0;
    }
}