using Firebase;
using Firebase.Auth;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AuthFirebase : MonoBehaviour
{
	public InputField Correo, Contrasena;
	
	public void Awake()
	{
		FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
			var dependencyStatus = task.Result;
			if (dependencyStatus == Firebase.DependencyStatus.Available) {
				// Create and hold a reference to your FirebaseApp, i.e.
				//   app = Firebase.FirebaseApp.DefaultInstance;
				// where app is a Firebase.FirebaseApp property of your application class.

				// Set a flag here indicating that Firebase is ready to use by your
				// application.
			} else {
				UnityEngine.Debug.LogError(System.String.Format(
					"Could not resolve all Firebase dependencies: {0}{1}", dependencyStatus, "Not reached"));
				// Firebase Unity SDK is not safe to use here.
			}
		});
	}

	public void LoginButtonPressed()
	{
		if (Correo.text.Length > 0 && Contrasena.text.Length > 0)
		{
			FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(Correo.text, Contrasena.text).ContinueWith((task) =>
			{
				if (task.IsCanceled) {
					Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
				}
				if (task.IsFaulted) {
					Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
				}

				FirebaseUser newUser = task.Result;
				Debug.LogFormat("User signed in successfully: {0} ({1})", newUser.DisplayName, newUser.UserId);
				//SceneManager.LoadSceneAsync("MainMenu");
				//SceneManager.LoadScene(3);
			});
			SceneManager.LoadSceneAsync("MainMenu");
		}
		else
		{
			Debug.LogError("El usuario o contraseña son incorrectos.");
		}
	}

	public void CreateNewUserButtonPressed()
	{
		if (Correo.text.Length > 0 && Contrasena.text.Length > 0)
		{
			FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(Correo.text, Contrasena.text).ContinueWith((task) =>
			{
				if (task.IsCanceled) {
					Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
				}
				if (task.IsFaulted) {
					Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
				}
				// Firebase user has been created.
				FirebaseUser newUser = task.Result;
				Debug.LogFormat("Firebase user created successfully: {0} ({1})", newUser.DisplayName, newUser.UserId);
				//SceneManager.LoadSceneAsync("MainMenu");
			});
			SceneManager.LoadSceneAsync("MainMenu");
		}
		else
		{
			Debug.LogError("Falta algún campo.");
		}
	}
}
