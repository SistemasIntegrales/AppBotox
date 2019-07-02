using Firebase;
using Firebase.Auth;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AuthFirebase : MonoBehaviour
{
	[SerializeField] private InputField Correo, Contrasena, _especialidad;
	private FirebaseApp _app;

	private FirebaseAuth auth;

	private FirebaseUser user;

	private bool _pase = false;
	
	public void Awake()
	{
		FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
			var dependencyStatus = task.Result;
			if (dependencyStatus == DependencyStatus.Available) {
				// Create and hold a reference to your FirebaseApp,
				// where app is a Firebase.FirebaseApp property of your application class.
				_app = FirebaseApp.DefaultInstance;

				// Set a flag here to indicate whether Firebase is ready to use by your app.
			} else {
				Debug.LogError(System.String.Format(
					"Could not resolve all Firebase dependencies: {0}", dependencyStatus));
				// Firebase Unity SDK is not safe to use here.
			}
		});
	}

	private void Update()
	{
		//Redireccion a Menu
		if (_pase)
		{
			SceneManager.LoadSceneAsync("MainMenu");
		}
	}

	public void LoginButtonPressed()
	{
		if (Correo.text.Length > 0 && Contrasena.text.Length > 0)
		{
			FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(Correo.text, Contrasena.text).ContinueWith(task => {
				if (task.IsCanceled) {
					Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
					return;
				}
				if (task.IsFaulted) {
					Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
					return;
				}

				_pase = true;

				FirebaseUser newUser = task.Result;
				Debug.LogFormat("User signed in successfully: {0} ({1})",
					newUser.DisplayName, newUser.UserId);
			});
		}
		else
		{
			Debug.LogError("El usuario o contraseña son incorrectos.");
		}
	}

	public void CreateNewUserButtonPressed()
	{
		if (Correo.text.Length > 0 && Contrasena.text.Length > 0 && _especialidad.text.Length > 0)
		{
			FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(Correo.text, Contrasena.text).ContinueWith(task => {
				if (task.IsCanceled) {
					Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
					return;
				}
				if (task.IsFaulted) {
					Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
					return;
				}
				
				_pase = true;

				// Firebase user has been created.
				FirebaseUser newUser = task.Result;
				Debug.LogFormat("Firebase user created successfully: {0} ({1})",
					newUser.DisplayName, newUser.UserId);
			});
		}
		else
		{
			Debug.LogError("Falta algún campo.");
		}
	}
	
	void InitializeFirebase() {
		auth = FirebaseAuth.DefaultInstance;
		auth.StateChanged += AuthStateChanged;
		AuthStateChanged(this, null);
	}

	void AuthStateChanged(object sender, System.EventArgs eventArgs) {
		if (auth.CurrentUser != user) {
			bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
			if (!signedIn && user != null) {
				Debug.Log("Signed out " + user.UserId);
			}
			user = auth.CurrentUser;
			if (signedIn) {
				Debug.Log("Signed in " + user.UserId);
				/* displayName = user.DisplayName ?? "";
				emailAddress = user.Email ?? "";
				photoUrl = user.PhotoUrl ?? ""; */
			}
		}
	}
}
