<?php
 require_once 'include/class.phpmailer.php';
 
 $mailto = 'info@estudiocarballido.com.ar'; // insert the <a href="send.php">send.php</a>email address you want the form sent to 
 $returnpage = 'index.html'; // insert the name of the page/location you want the user to be returned to 
 $sitename = '[EstudioCarballido.com.ar]'; // insert the site name here, it will appear in the subject of your email 
 
 /* Do not edit below this line unless you know what you're doing */ 

  $name = $_POST['name']; 
  $email = $_POST['email']; 
  $telefono = $_POST['telefono']; 
  $enquiry = stripslashes($_POST['query']); 
  $message = "<p>$name te ha enviado el siguiente mensaje: <p><p>
  				 $enquiry<p><p> 
                 Sus datos son:<p><p>
				 Nombre: $name<p>
				 Correo: $email<p>
				 Teléfono: $telefono<p><p>"; 

 
        if (!$name) { 
                 print("<strong>Error:</strong> Escriba su nombre.<br/><br/><a href='java script:history.go(-1)'>Regresar</a>"); 
                 exit; 
         } 
         if (!$email) { 
                 print("<strong>Error:</strong> Porfavor escriba su e-mail.<br/><br/><a href='java script:history.go(-1)'>Regresar</a>"); 
                 exit; 
         } 
         if (!$enquiry) { 
                 print("<strong>Error:</strong> Porfavor escriba un mensaje.<br/><br/><a href='java script:history.go(-1)'>Regresar</a>");                 exit; 
         } 
         if (!preg_match("/^[a-z0-9]+([-_\.]?[a-z0-9])+@[a-z0-9]+([-_\.]?[a-z0-9])+\.[a-z]{2,4}/", $email)){ 
         print("<strong>Error:</strong> este email no es valido, favor escribir su e-mail el correcto.<br/><br/><a href='java 		script:history.go(-1)'>Regresar</a>"); 
                 exit; 
        }        
        if (!$telefono) { 
                print("<strong>Error:</strong> Porfavor escriba su telefono.<br/><br/><a href='java script:history.go(-1)'>Regresar</a>");                 exit; 
        } 

    //$conexion = mysql_connect("localhost", "damarsan", "luz3blanco");
    //mysql_select_db("damarsan_infoviajes", $conexion);

    //$quecontacto = sprintf("SELECT * FROM contactos where (con_mail = %s)", sanitize($email, "text"));
    //$rescontacto = mysql_query($quecontacto, $conexion) or die(mysql_error());
    //$totcontacto = mysql_num_rows($rescontacto);
    
    //if ($totcontacto < 1) {

    //$quecont = "INSERT INTO contactos (con_nom, con_mail, con_tel) VALUES ($name, $email, $telefono)";
    //$quecont = sprintf("INSERT INTO contactos (con_nom, con_mail, con_tel) VALUES (%s, %s, %s)",
//						sanitize($name, "text"),
	//					sanitize($email, "text"),
//						sanitize($telefono, "text"));
    //$rescont = mysql_query($quecont, $conexion) or die(mysql_error());
   // }   

// $mail->PluginDir = "include/";
 $mail = new PHPMailer;
 $mail->IsSmtp();                                      // Set mailer to use SMTP

$mail->Host = "mail.estudiocarballido.com.ar";  // Specify main and backup server
$mail->port = 26; //26 --- 587;
$mail->SMTPAuth = true;                               // Enable SMTP authentication
$mail->Username = "info@estudiocarballido.com.ar";                            // SMTP username
$mail->Password = "Luz3blanco!";                           // SMTP password
//$mail->SMTPSecure = "TLS";                            // Enable encryption, 'ssl' also accepted
$mail->SMTPDebug = 0;

$mail->AddAddress($mailto);
$mail->From = $email;
$mail->FromName = $name;
$mail->MsgHTML($message);
//$mail->Body    = 'This is the HTML message body <b>in bold!</b>';
//$mail->AddAddress('josh@example.net', 'Josh Adams');  // Add a recipient
//$mail->AddAddress('ellen@example.com');               // Name is optional
//$mail->AddReplyTo('info@example.com', 'Information');
//$mail->AddCC('cc@example.com');
//$mail->AddBCC('bcc@example.com');
$mail->Timeout=30;

//$mail->WordWrap = 50;                                 // Set word wrap to 50 characters
//$mail->AddAttachment('/var/tmp/file.tar.gz');         // Add attachments
//$mail->AddAttachment('/tmp/image.jpg', 'new.jpg');    // Optional name
$mail->IsHTML(true);                                  // Set email format to HTML
$mail->Subject = 'EstudioCarballido.com.ar - Consulta desde el Sitio';
$mail->AltBody = 'Consulta desde el Sitio Web EstudioCarballido.com.ar, HTML no habilitado';

//la variable $exito tendra el valor true
  $exito = $mail->Send();
   if(!$exito)
   {
	echo "Problemas enviando correo electrónico";
	echo "<br/>".$mail->ErrorInfo;	
   }
   else
   {
	//header("Location: " .$returnpage);
	echo "<center><strong><br/><br/>Gracias por contactarse con nosotros<br/>";	
	echo "<br/>Su mensaje ha sido enviado correctamente<br/>";	
	echo "<br/>Nos pondremos en contacto a la brevedad<br/><br/>";	
	echo "<br/><br/>En un instante será redireccionado al Home<br/><br/></strong></center>";
   } 


//  mail($mailto, "$sitename Consulta de $name", $message, "From: $email"); 
  
//  header("Location: " .$returnpage); 

        
/*************************************/
/*Esto es para parsear las variables */
/*************************************/
function sanitize($value, $type)
{
  $value = (!get_magic_quotes_gpc()) ? addslashes($value) : $value;

  switch ($type) {
    case "text":
      $value = ($value != "") ? "'" . $value . "'" : "NULL";
      break;
    case "long":
    case "int":
      $value = ($value != "") ? intval($value) : "NULL";
      break;
    case "double":
      $value = ($value != "") ? "'" . doubleval($value) . "'" : "NULL";
      break;
    case "date":
      $value = ($value != "") ? "'" . $value . "'" : "NULL";
      break;
  }
  return $value;
}

  ?>	
 <script language="javascript">
    setTimeout(function(){location.href="index.html"} , 5000);

	</script>