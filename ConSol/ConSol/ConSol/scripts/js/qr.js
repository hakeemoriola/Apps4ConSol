function read(a)
{
    $("#qr-value").text(a);
    alert(a);
}

qrcode.callback = read;