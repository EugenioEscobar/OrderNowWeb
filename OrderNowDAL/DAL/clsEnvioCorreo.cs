using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNowDAL.Correo
{
    public class clsEnvioCorreo
    {
        private readonly string from = "ordernowpedidos@gmail.com";
        private readonly string password = "Ordernow666";

        public void EnviarMensaje(string correoCliente, string valor, string nombre, string id, string fecha)
        {
            //esta clase genera todo lo necesario para el envio del correo de confirmacion, recibe los valores directamente desde el carrito
            // y luego los inserta dentro del formato hmtl ya definido

            var mensaje = new MimeMessage();
            mensaje.From.Add(new MailboxAddress("OrderNow", from));
            mensaje.To.Add(new MailboxAddress("Cliente", correoCliente));
            mensaje.Subject = "Confirmacion de pedido";

            var builder = new BodyBuilder();

            builder.HtmlBody = string.Format($@"<table style='max-width: 600px; padding: 10px; margin:0 auto; border-collapse: collapse;'>
                    <tr>
                        <td style='padding: 0'>
			                <img src='https://i.ibb.co/51NV72k/comida-1.jpg' style='padding: 0; display: block'  width='100%' height='85%' >
		                </td>
	                </tr>
                   	<tr>
						<td style='background-color: #ecf0f1'>
						<div style='color: #34495e; margin: 4% 10% 2%; text-align: justify;font-family: sans-serif'>
							<h2 style='color: #0d1be4e8; margin: 0 0 7px'>¡Tu pedido ya esta siendo preparado!</h2>
								<p style='margin: 2px; font-size: 15px'>
									Somos OrderNow esperamos que quedes satisfecho con el pedido realizado.
								</p><br>
								<ul style='font-size: 15px;  margin: 10px '>
									<li>Id Pedido:{id}</li>
									<li>Nombre:{nombre}</li>
									<li>Fecha:{fecha}</li>
									<li>Telefono de contacto:+5698444444</li>
									<li>Total:{valor}</li>
								</ul><br> <br>
				<p style='color:#b3b3b3; margin: 10px; font-size: 12px'> Este correo puede ser solicitado por el personal si tu pedido es para retirar en el local.</p>
					<div style='width: 100%;margin:20px 0; display: inline-block;text-align: center'>
						<img style='padding: 0; width: 100px; margin: 20px' src='https://i.ibb.co/sq05dVH/logo.png'>
					
					</div>
						<div style='width: 100%; text-align: center'>
							<a style='text-decoration: none; border-radius: 5px; padding: 11px 23px; color: white; background-color: #0d1be4e8' href='ordernow.com'>Ir a la página</a>	
						</div>
					<p style='color: #b3b3b3; font-size: 12px; text-align: center;margin: 30px 0 0'>OrderNow (DVE TEAM) &copy; 2020</p>
						</div>
					</td>
					</tr>
						</table> ");
            mensaje.Body = builder.ToMessageBody();
            using (var client = new SmtpClient())
            {

                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect("smtp.gmail.com", 587);
                client.Authenticate(from, password);
                client.Send(mensaje);
                client.Disconnect(true);
            }
        }
    }
}
