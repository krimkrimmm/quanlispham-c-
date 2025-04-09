public interface IRoleService
{
	Task AddRoleAsync(string roleName);
	Task RemoveRoleAsync(string roleName);
}
