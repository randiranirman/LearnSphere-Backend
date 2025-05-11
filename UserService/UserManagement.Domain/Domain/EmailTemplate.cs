public static class EmailTemplates
{
    public static string WelcomeSubject => "Welcome to LeanSphere!";

    public static string WelcomeBody(string name, string username, string password)
    {
        return $@"
Hi {name},

Welcome to LearnSphere! Your account has been successfully created.

Here are your login details:
Username: {username}
Password: {password}

Please log in to your account and change your password as soon as possible for security reasons.

You can log in here: https://yourappdomain.com/login

If you have any questions, feel free to reach out.

Thanks,  
The LeanSphere Team
";
    }
}
