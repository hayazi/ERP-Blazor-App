using ERPBlazorApp.AAA.Models;
using ERPBlazorApp.AAA.Services;
using Microsoft.AspNetCore.Components;
using Serilog;

namespace ERPBlazorApp.AAA.Auth;

public class AuthService
{
    private readonly UserService _userService;
    private readonly NavigationManager _navigationManager;
    private static readonly Serilog.ILogger Logger = Serilog.Log.ForContext<AuthService>();

    public event Action? OnAuthStateChanged;

    public User? CurrentUser { get; private set; }
    public bool IsAuthenticated => CurrentUser != null;

    public AuthService(UserService userService, NavigationManager navigationManager)
    {
        _userService = userService;
        _navigationManager = navigationManager;
    }

    public async Task<bool> LoginAsync(string username, string password)
    {
        Logger.Information("Login attempt for username {Username}", username);
        var user = await _userService.GetByUsernameAsync(username);
        if (user != null && user.PasswordHash == password && user.IsActive)
        {
            CurrentUser = user;
            Logger.Information("User {Username} logged in successfully", username);
            OnAuthStateChanged?.Invoke();
            return true;
        }

        Logger.Warning("Failed login attempt for username {Username}", username);
        return false;
    }

    public void Logout()
    {
        Logger.Information("User {Username} logged out", CurrentUser?.Username);
        CurrentUser = null;
        OnAuthStateChanged?.Invoke();
        _navigationManager.NavigateTo("/");
    }

    public Task<User?> GetCurrentUserAsync()
    {
        return Task.FromResult(CurrentUser);
    }
}
