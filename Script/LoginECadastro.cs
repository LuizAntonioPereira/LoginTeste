using Godot;
using System;
using static Godot.LineEdit;

public class LoginECadastro : Control
{	
	private Label texto2 = new Label();
	private PanelContainer con = new PanelContainer();
	
	private LineEdit ed = new LineEdit();
	private LineEdit ed2 = new LineEdit();
	private LineEdit ed3 = new LineEdit();
	private LineEdit ed4 = new LineEdit();
	
	private Button btn1 = new Button();
	private Button btn2 = new Button();
	private Button btn3 = new Button();
	private Button btn4 = new Button();
	
	private LineEdit ed5 = new LineEdit();
	private LineEdit ed6 = new LineEdit();
	private LineEdit ed7 = new LineEdit();
	private LineEdit ed8 = new LineEdit();
	private AlignEnum alinhamento = AlignEnum.Center;
	private bool visivel = false;
	
	private enum TipoLC {Login, Cadastro};
	private TipoLC Tipo = TipoLC.Login;
	
	private const string UrlEnviar = "http:";
	private const string UrlCadastrar = "http:";

	private string Senha;
	private string Nome;
	
	private HTTPRequest RequestEn = new HTTPRequest();
	private HTTPRequest RequestRe = new HTTPRequest();
	
	public override void _Ready()
	{
		RequestEn = GetNode<HTTPRequest>("HTTPRequestEn");
		RequestEn.Connect("request_completed", this,nameof(Enviar));
		
		RequestRe = GetNode<HTTPRequest>("HTTPRequestRe");
		RequestRe.Connect("request_completed", this,nameof(Receber));
		
		AddChild(con);
		AddChild(texto2);
		AddChild(ed);
		AddChild(ed2);
		AddChild(ed3);
		AddChild(ed4);
		AddChild(btn1);
		AddChild(btn2);
		AddChild(ed5);
		AddChild(ed6);
		AddChild(ed7);
		AddChild(ed8);
		AddChild(btn3);
		AddChild(btn4);
		
		//Texto
		texto2.SetPosition(new Vector2(350,205));

		//Painel
		con.SetPosition(new Vector2(250,200));
		con.SetSize(new Vector2(350,200));
		
		btn1.Connect("pressed", this, nameof(Logar));
		btn4.Connect("pressed", this, nameof(Informe), new Godot.Collections.Array(){"Login"});

		btn2.Connect("pressed", this, nameof(Pressionar), new Godot.Collections.Array(){"Cadastro"});
		btn3.Connect("pressed", this, nameof(Pressionar), new Godot.Collections.Array(){"Login"});
	}

	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		switch(Tipo){
				
				case TipoLC.Login:{
					
					btn3.Visible = visivel;
					btn4.Visible = visivel;
					btn1.Visible = !visivel;
					btn2.Visible = !visivel;
					ed5.Visible = visivel;
					ed6.Visible = visivel;
					ed7.Visible = visivel;
					ed8.Visible = visivel;
					//LineEdit Login
					ed.SetPosition(new Vector2(300,260));
					ed.SetSize(new Vector2(120,20));
					ed.Align = alinhamento;
					ed.Text = "Login";
					ed.Editable = false;
					ed.SelectingEnabled = false;

					ed2.SetPosition(new Vector2(300,290));
					ed2.SetSize(new Vector2(120,20));
					ed2.Align = alinhamento;
					ed2.Text = "Senha";
					ed2.Editable = false;
					ed2.SelectingEnabled = false;

					//LineEdit Recebe Variaveis
					//Login
					ed3.SetPosition(new Vector2(450,260));
					ed3.SetSize(new Vector2(120,20));

					//Senha
					ed4.SetPosition(new Vector2(450,290));
					ed4.SetSize(new Vector2(120,20));
					ed4.Align = alinhamento;
					ed4.Secret = true;
					ed4.SecretCharacter = "$";

					//Button
					btn1.SetPosition(new Vector2(310,355));
					btn1.SetSize(new Vector2(80,25));
					btn1.Text = "Logar";

					btn2.SetPosition(new Vector2(460,355));
					btn2.SetSize(new Vector2(80,25));
					btn2.Text = "Cadastrar";
					
				}break;
				
				case TipoLC.Cadastro:{
					
					btn3.Visible = !visivel;
					btn4.Visible = !visivel;
					btn1.Visible = visivel;
					btn2.Visible = visivel;
					ed5.Visible = !visivel;
					ed6.Visible = !visivel;
					ed7.Visible = !visivel;
					ed8.Visible = !visivel;
					//LineEdit Login
					ed.SetPosition(new Vector2(300,230));
					ed.SetSize(new Vector2(120,20));
					ed.Align = alinhamento;
					ed.Text = "Login";
					ed.Editable = false;
					ed.SelectingEnabled = false;

					ed5.SetPosition(new Vector2(300,260));
					ed5.SetSize(new Vector2(120,20));
					ed5.Align = alinhamento;
					ed5.Text = "Email";
					ed5.Editable = false;
					ed5.SelectingEnabled = false;
					
					ed2.SetPosition(new Vector2(300,290));
					ed2.SetSize(new Vector2(120,20));
					ed2.Align = alinhamento;
					ed2.Text = "Senha";
					ed2.Editable = false;
					ed2.SelectingEnabled = false;

					ed6.SetPosition(new Vector2(300,320));
					ed6.SetSize(new Vector2(120,20));
					ed6.Align = alinhamento;
					ed6.Text = "Repita a Senha";
					ed6.Editable = false;
					ed6.SelectingEnabled = false;
					
					//LineEdit Recebe Variaveis
					//Login
					ed3.SetPosition(new Vector2(450,230));
					ed3.SetSize(new Vector2(120,20));

					//Email
					ed7.SetPosition(new Vector2(450,260));
					ed7.SetSize(new Vector2(120,20));
					
					//Senha
					ed4.SetPosition(new Vector2(450,290));
					ed4.SetSize(new Vector2(120,20));
					ed4.Align = alinhamento;
					ed4.Secret = true;
					ed4.SecretCharacter = "$";
					
					//Repitir senha
					ed8.SetPosition(new Vector2(450,320));
					ed8.SetSize(new Vector2(120,20));
					ed8.Align = alinhamento;
					ed8.Secret = true;
					ed8.SecretCharacter = "$";
					
					//Button
					btn3.SetPosition(new Vector2(310,355));
					btn3.SetSize(new Vector2(80,25));
					btn3.Text = "Voltar";
					
					btn4.SetPosition(new Vector2(460,355));
					btn4.SetSize(new Vector2(80,25));
					btn4.Text = "Salvar";
					
				}break;
		}
	}
	private void Logar(){
		
		var query = "nome=" + ed3.Text + "senha=" + ed4.Text;
		string[] headers = new string[] { "Content-Type: application/json" };
		RequestRe.Request("teste123" + query , headers , false);
		
	}
	
	public void Pressionar(string tela){
		
		Tipo = (TipoLC)Enum.Parse(typeof(TipoLC), tela);
	}

	private void Informe(string Case)
	{
		var query = "nome=" + ed3.Text + "senha=" + ed4.Text;
		string[] headers = new string[] { "Content-Type: application/json" };
		RequestEn.Request("teste123", headers , false , HTTPClient.Method.Post , query);
		Tipo = (TipoLC)Enum.Parse(typeof(TipoLC), Case);
	}
	private void Enviar(int result, int response_code, string[] headers, byte[] body){
		
		JSONParseResult json = JSON.Parse( System.Text.UTF8Encoding.Default.GetString(body));
		
		if (result == (int)HTTPRequest.Result.Success){
			
			if (response_code == 200){
				
				// aceitarHighScore();
				
			}else{
				
				//negarHighScore();
				
			}
			
		}

	}
	
	private void Receber(int result, int response_code, string[] headers, byte[] body){
		
		JSONParseResult json = JSON.Parse( System.Text.UTF8Encoding.Default.GetString(body));
		
		if (result == (int)HTTPRequest.Result.Success){
			
			if (response_code == 200){
				
				// aceitarHighScore();
				
			}else{
				
				//negarHighScore();
				
			}
			
		}

	}
}
