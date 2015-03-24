rmdir /s /q "TestResults"
rmdir /s /q "results"
rmdir /s /q "coverage"
mkdir "results"
mkdir "coverage"


packages\OpenCover.4.5.3723\OpenCover.Console.exe^
 -register:user^
 -target:"C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe"^
 -targetargs:"""CakeMail.RestClient.UnitTests\bin\Debug\CakeMail.RestClient.UnitTests.dll"""^
 -filter:"+[CakeMail.RestClient]* -[CakeMail.RestClient]CakeMail.RestClient.Models.* -[CakeMail.RestClient]CakeMail.RestClient.Properties.*"^
 -excludebyattribute:*.ExcludeFromCodeCoverage*^
 -hideskipped:All^
 -output:.\coverage\CakeMail.RestClient_coverage.xml

 
rmdir /s /q "report"
mkdir "report"

packages\ReportGenerator.2.1.4.0\ReportGenerator.exe^
 -reports:.\coverage\*.xml^
 -targetdir:.\report^
 -reporttypes:Html^
 -filters:-CakeMail.RestClient.UnitTests*


start report\index.htm