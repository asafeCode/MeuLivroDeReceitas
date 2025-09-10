ä
€C:\Users\User\Desktop\ProjAsafe\MeuLivroDeReceitas\src\Shared\MyRecipeBook.Communication\Responses\ResponseRegisteredUserJson.cs
	namespace 	
MyRecipeBook
 
. 
Communication $
.$ %
	Responses% .
;. /
public 
class &
ResponseRegisteredUserJson '
{ 
public 

string 
Name 
{ 
get 
; 
set !
;! "
}# $
=% &
String' -
.- .
Empty. 3
;3 4
} þ
wC:\Users\User\Desktop\ProjAsafe\MeuLivroDeReceitas\src\Shared\MyRecipeBook.Communication\Responses\ResponseErrorJson.cs
	namespace 	
MyRecipeBook
 
. 
Communication $
.$ %
	Responses% .
;. /
public 
class 
ResponseErrorJson 
{ 
public 

IList 
< 
string 
> 
Errors 
{  !
get" %
;% &
set' *
;* +
}, -
public 

ResponseErrorJson 
( 
IList "
<" #
string# )
>) *
errors+ 1
)1 2
=>3 5
Errors6 <
== >
errors? E
;E F
public		 

ResponseErrorJson		 
(		 
string		 #
error		$ )
)		) *
{

 
Errors 
= 
new 
List 
< 
string  
>  !
{ 	
error 
} 	
;	 

} 
} ¸
|C:\Users\User\Desktop\ProjAsafe\MeuLivroDeReceitas\src\Shared\MyRecipeBook.Communication\Requests\RequestUserRegisterJson.cs
	namespace 	
MyRecipeBook
 
. 
Communication $
.$ %
Requests% -
;- .
public 
class #
RequestUserRegisterJson $
{ 
public 

string 
Name 
{ 
get 
; 
set !
;! "
}# $
=% &
string' -
.- .
Empty. 3
;3 4
public 

string 
Email 
{ 
get 
; 
set "
;" #
}$ %
=& '
string( .
.. /
Empty/ 4
;4 5
public 

string 
Password 
{ 
get  
;  !
set" %
;% &
}' (
=) *
string+ 1
.1 2
Empty2 7
;7 8
} 