namespace BRSS66.ApplicationCore.Entities;

public class Password
{
    private static readonly Random Random = new Random();

    private const string AllowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_+-=[]{}|;:,.<>?";

    private readonly int _length;

    public Password(int length)
    {
        this._length = length;
    }

    public string Next()
    {
        char[] password = new char[_length];

        for (int i = 0; i < _length; i++)
        {
            password[i] = AllowedChars[Random.Next(0, AllowedChars.Length)];
        }

        return new string(password);
    }
}