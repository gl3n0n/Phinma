<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<script src="Scripts/jquery-1.5.1.js"></script>
<script type= "text/javascript">
    $(document).ready(function () {

        function update() {
            $.ajax({
                type: 'POST',
                url: 'timer.aspx',
                timeout: 60000,
                success: function (data) {
                    $("#timer").html(data);
                    window.setTimeout(update, 60000);
                },
                async:false,
            });
        }
        update();
    });
    </script>
    <style type="text/css">
        body {
            background-color:#67893F;
            color:#ffffff;
            margin:0px;
            margin-top:3px;
            margin-left:25px;
            font-family:Arial;
            font-weight:bold;
            font-size:11px;
            background:url(images/clock1.jpg) no-repeat left;
            background-color:#67893F;
        }
    </style>
</head>
<body>
<div id="timer"></div>
</body>
</html>
