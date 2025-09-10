Ä
€C:\Users\User\Desktop\ProjAsafe\MeuLivroDeReceitas\src\Backend\MyRecipeBook.Domain\Repositories\User\IUserWriteOnlyRepository.cs
	namespace 	
MyRecipeBook
 
. 
Domain 
. 
Repositories *
.* +
User+ /
;/ 0
public 
	interface $
IUserWriteOnlyRepository )
{ 
public 

Task 
Add 
( 
Entities 
. 
User !
user" &
)& '
;' (
} å
C:\Users\User\Desktop\ProjAsafe\MeuLivroDeReceitas\src\Backend\MyRecipeBook.Domain\Repositories\User\IUserReadOnlyRepository.cs
	namespace 	
MyRecipeBook
 
. 
Domain 
. 
Repositories *
.* +
User+ /
;/ 0
public 
	interface #
IUserReadOnlyRepository (
{ 
public 

Task 
< 
bool 
> %
ExistsActiveUserWithEmail /
(/ 0
string0 6
email7 <
)< =
;= >
} ã
sC:\Users\User\Desktop\ProjAsafe\MeuLivroDeReceitas\src\Backend\MyRecipeBook.Domain\Repositories\User\IUnitOfWork.cs
	namespace 	
MyRecipeBook
 
. 
Domain 
. 
Repositories *
.* +
User+ /
;/ 0
public 
	interface 
IUnitOfWork 
{ 
public 

Task 
Commit 
( 
) 
; 
} Ü
pC:\Users\User\Desktop\ProjAsafe\MeuLivroDeReceitas\src\Backend\MyRecipeBook.Domain\Extensions\StringExtension.cs
	namespace 	
MyRecipeBook
 
. 
Domain 
. 

Extensions (
;( )
public 
static 
class 
StringExtension #
{ 
public 

static 
bool 
NotEmpty 
(  
[  !
NotNullWhen! ,
(, -
true- 1
)1 2
]2 3
this3 7
string8 >
?> ?
value@ E
)E F
=>G I
string 
. 
IsNullOrWhiteSpace !
(! "
value" '
)' (
.( )
IsFalse) 0
(0 1
)1 2
;2 3
}

 Ñ
qC:\Users\User\Desktop\ProjAsafe\MeuLivroDeReceitas\src\Backend\MyRecipeBook.Domain\Extensions\BooleanExtension.cs
	namespace 	
MyRecipeBook
 
. 
Domain 
. 

Extensions (
;( )
public 
static 
class 
BooleanExtension $
{ 
public 

static 
bool 
IsFalse 
( 
this #
bool$ (
value) .
). /
=>0 2
!3 4
value4 9
;9 :
} ¬
cC:\Users\User\Desktop\ProjAsafe\MeuLivroDeReceitas\src\Backend\MyRecipeBook.Domain\Entities\User.cs
	namespace 	
MyRecipeBook
 
. 
Domain 
. 
Entities &
;& '
public		 
class		 
User		 
:		 

EntityBase		 
{

 
public 

string 
Name 
{ 
get 
; 
set !
;! "
}# $
=% &
string' -
.- .
Empty. 3
;3 4
public 

string 
Email 
{ 
get 
; 
set "
;" #
}$ %
=& '
string( .
.. /
Empty/ 4
;4 5
public 

string 
Password 
{ 
get  
;  !
set" %
;% &
}' (
=) *
string+ 1
.1 2
Empty2 7
;7 8
} š
iC:\Users\User\Desktop\ProjAsafe\MeuLivroDeReceitas\src\Backend\MyRecipeBook.Domain\Entities\EntityBase.cs
	namespace 	
MyRecipeBook
 
. 
Domain 
. 
Entities &
;& '
public 
class 

EntityBase 
{ 
public 

long 
Id 
{ 
get 
; 
set 
; 
}  
public 

bool 
Active 
{ 
get 
; 
set !
;! "
}# $
=% &
true' +
;+ ,
public 

DateTime 
	CreatedOn 
{ 
get  #
;# $
set% (
;( )
}* +
=, -
DateTime. 6
.6 7
UtcNow7 =
;= >
} 