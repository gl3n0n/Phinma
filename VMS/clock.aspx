<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<script src="Scripts/jquery-latest.min.js"></script>
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
            color:#fff;
            margin:0px;
            margin-top:3px;
            margin-left:25px;
            font-family:Arial;
            font-size:10px;
            font-weight:normal;
            padding:0px;
            
        }
        div#timer 
        {
            padding:0px;
            margin:0px;
            font-weight:normal;
        }
    </style>
</head>
<body>
<div id="timer"></div>
</body>
</html>
