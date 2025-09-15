ß
‡C:\Users\User\Desktop\ProjAsafe\MeuLivroDeReceitas\src\Backend\MyRecipeBook.Application\UseCases\User\Register\RegisterUserValidator.cs
	namespace 	
MyRecipeBook
 
. 
Application "
." #
UseCases# +
.+ ,
User, 0
.0 1
Register1 9
;9 :
public		 
class		 !
RegisterUserValidator		 "
:		# $
AbstractValidator		% 6
<		6 7#
RequestUserRegisterJson		7 N
>		N O
{

 
public 
!
RegisterUserValidator  
(  !
)! "
{ 
RuleFor 
( 
user 
=> 
user 
. 
Name !
)! "
." #
NotEmpty# +
(+ ,
), -
.- .
WithMessage. 9
(9 :%
ResourceMessagesException: S
.S T

NAME_EMPTYT ^
)^ _
;_ `
RuleFor 
( 
user 
=> 
user 
. 
Email "
)" #
.# $
NotEmpty$ ,
(, -
)- .
.. /
WithMessage/ :
(: ;%
ResourceMessagesException; T
.T U
EMAIL_EMPTYU `
)` a
;a b
RuleFor 
( 
user 
=> 
user 
. 
Password %
.% &
Length& ,
), -
.- . 
GreaterThanOrEqualTo. B
(B C
$numC D
)D E
.E F
WithMessageF Q
(Q R%
ResourceMessagesExceptionR k
.k l
PASSWORD_EMPTYl z
)z {
;{ |
When 
( 
user 
=> 
user 
. 
Email 
.  
NotEmpty  (
(( )
)) *
,* +
(, -
)- .
=>/ 1
{ 	
RuleFor 
( 
user 
=> 
user  
.  !
Email! &
)& '
.' (
EmailAddress( 4
(4 5
)5 6
.6 7
WithMessage7 B
(B C%
ResourceMessagesExceptionC \
.\ ]
EMAIL_INVALID] j
)j k
;k l
} 	
)	 

;
 
} 
} ²)
…C:\Users\User\Desktop\ProjAsafe\MeuLivroDeReceitas\src\Backend\MyRecipeBook.Application\UseCases\User\Register\RegisterUserUseCase.cs
	namespace 	
MyRecipeBook
 
. 
Application "
." #
UseCases# +
.+ ,
User, 0
.0 1
Register1 9
;9 :
public 
class 
RegisterUserUseCase  
:! " 
IRegisterUserUseCase# 7
{ 
private 
readonly #
IUserReadOnlyRepository ,
_readOnlyRepository- @
;@ A
private 
readonly $
IUserWriteOnlyRepository - 
_writeOnlyRepository. B
;B C
private 
readonly 
IMapper 
_mapper $
;$ %
private 
readonly 
PasswordEncripter &
_passwordEncripter' 9
;9 :
private 
readonly 
IUnitOfWork  
_unitOfWork! ,
;, -
public 

RegisterUserUseCase 
( #
IUserReadOnlyRepository 
readOnlyRepository  2
,2 3$
IUserWriteOnlyRepository  
writeOnlyRepository! 4
,4 5
IMapper 
mapper 
, 
PasswordEncripter 
passwordEncripter +
,+ ,
IUnitOfWork 

unitOfWork 
) 
{ 
_readOnlyRepository 
= 
readOnlyRepository 0
;0 1 
_writeOnlyRepository 
= 
writeOnlyRepository 2
;2 3
_mapper   
=   
mapper   
;   
_passwordEncripter!! 
=!! 
passwordEncripter!! .
;!!. /
_unitOfWork"" 
="" 

unitOfWork""  
;""  !
}$$ 
public&& 

async&& 
Task&& 
<&& &
ResponseRegisteredUserJson&& 0
>&&0 1
Execute&&2 9
(&&9 :#
RequestUserRegisterJson&&: Q
request&&R Y
)&&Y Z
{'' 
await)) 
Validate)) 
()) 
request)) 
))) 
;))  
var++ 
user++ 
=++ 
_mapper++ 
.++ 
Map++ 
<++ 
Domain++ %
.++% &
Entities++& .
.++. /
User++/ 3
>++3 4
(++4 5
request++5 <
)++< =
;++= >
user.. 
... 
Password.. 
=.. 
_passwordEncripter.. *
...* +
Encrypt..+ 2
(..2 3
request..3 :
...: ;
Password..; C
)..C D
;..D E
await11  
_writeOnlyRepository11 "
.11" #
Add11# &
(11& '
user11' +
)11+ ,
;11, -
await22 
_unitOfWork22 
.22 
Commit22  
(22  !
)22! "
;22" #
return66 
new66 &
ResponseRegisteredUserJson66 -
{77 	
Name88 
=88 
user88 
.88 
Name88 
,88 
}:: 	
;::	 

};; 
private== 
async== 
Task== 
Validate== 
(==  #
RequestUserRegisterJson==  7
request==8 ?
)==? @
{>> 
var?? 
	validator?? 
=?? 
new?? !
RegisterUserValidator?? 1
(??1 2
)??2 3
;??3 4
varAA 
resultAA 
=AA 
	validatorAA 
.AA 
ValidateAA '
(AA' (
requestAA( /
)AA/ 0
;AA0 1
varCC 
emailExistsCC 
=CC 
awaitCC 
_readOnlyRepositoryCC  3
.CC3 4%
ExistsActiveUserWithEmailCC4 M
(CCM N
requestCCN U
.CCU V
EmailCCV [
)CC[ \
;CC\ ]
ifEE 

(EE 
emailExistsEE 
)EE 
resultFF 
.FF 
ErrorsFF 
.FF 
AddFF 
(FF 
newFF !
ValidationFailureFF" 3
(FF3 4
stringFF4 :
.FF: ;
EmptyFF; @
,FF@ A%
ResourceMessagesExceptionFFB [
.FF[ \$
EMAIL_ALREADY_REGISTEREDFF\ t
)FFt u
)FFu v
;FFv w
ifII 

(II 
resultII 
.II 
IsValidII 
.II 
IsFalseII "
(II" #
)II# $
)II$ %
{JJ 	
varKK 
errorMessagesKK 
=KK 
resultKK  &
.KK& '
ErrorsKK' -
.KK- .
SelectKK. 4
(KK4 5
eKK5 6
=>KK7 9
eKK: ;
.KK; <
ErrorMessageKK< H
)KKH I
.KKI J
ToListKKJ P
(KKP Q
)KKQ R
;KKR S
throwMM 
newMM &
ErrorOnValidationExceptionMM 0
(MM0 1
errorMessagesMM1 >
)MM> ?
;MM? @
}OO 	
}PP 
}QQ §
†C:\Users\User\Desktop\ProjAsafe\MeuLivroDeReceitas\src\Backend\MyRecipeBook.Application\UseCases\User\Register\IRegisterUserUseCase.cs
	namespace 	
MyRecipeBook
 
. 
Application "
." #
UseCases# +
.+ ,
User, 0
.0 1
Register1 9
;9 :
public 
	interface  
IRegisterUserUseCase %
{ 
public 

Task 
< &
ResponseRegisteredUserJson *
>* +
Execute, 3
(3 4#
RequestUserRegisterJson4 K
requestL S
)S T
;T U
} Ò
‚C:\Users\User\Desktop\ProjAsafe\MeuLivroDeReceitas\src\Backend\MyRecipeBook.Application\Services\Cryptography\PasswordEncripter.cs
	namespace 	
MyRecipeBook
 
. 
Application "
." #
Services# +
.+ ,
Cryptography, 8
;8 9
public 
class 
PasswordEncripter 
{ 
private 
readonly 
string 
_additionalKey *
;* +
public		 

PasswordEncripter		 
(		 
string		 #
additionalKey		$ 1
)		1 2
=>		3 5
_additionalKey		6 D
=		E F
additionalKey		G T
;		T U
public

 

string

 
Encrypt

 
(

 
string

  
password

! )
)

) *
{ 
var 
newPassword 
= 
$" 
{ 
password %
}% &
{& '
_additionalKey' 5
}5 6
"6 7
;7 8
var 

bytes 
= 
Encoding 
. 
UTF8  
.  !
GetBytes! )
() *
newPassword* 5
)5 6
;6 7
var 
	hashBytes 
= 
SHA512 
. 
HashData '
(' (
bytes( -
)- .
;. /
return 
StringBytes 
( 
	hashBytes $
)$ %
;% &
} 
private 
static 
string 
StringBytes %
(% &
byte& *
[* +
]+ ,
bytes- 2
)2 3
{ 
var 
sb 
= 
new 
StringBuilder "
(" #
)# $
;$ %
foreach 
( 
byte 
b 
in 
bytes  
)  !
{ 	
var 
hex 
= 
b 
. 
ToString  
(  !
$str! %
)% &
;& '
sb 
. 
Append 
( 
hex 
) 
; 
} 	
return 
sb 
. 
ToString 
( 
) 
; 
} 
}!! ì	
zC:\Users\User\Desktop\ProjAsafe\MeuLivroDeReceitas\src\Backend\MyRecipeBook.Application\Services\AutoMapper\AutoMapping.cs
	namespace 	
MyRecipeBook
 
. 
Application "
." #
Services# +
.+ ,

AutoMapper, 6
;6 7
public 
class 
AutoMapping 
: 
Profile "
{ 
public 

AutoMapping 
( 
) 
{ 
RequestToDomain		 
(		 
)		 
;		 
}

 
private 
void 
RequestToDomain  
(  !
)! "
{ 
	CreateMap 
< #
RequestUserRegisterJson )
,) *
Domain+ 1
.1 2
Entities2 :
.: ;
User; ?
>? @
(@ A
)A B
. 
	ForMember 
( 
dest 
=> 
dest #
.# $
Password$ ,
,, -
opt. 1
=>2 4
opt5 8
.8 9
Ignore9 ?
(? @
)@ A
)A B
;B C
} 
} §
wC:\Users\User\Desktop\ProjAsafe\MeuLivroDeReceitas\src\Backend\MyRecipeBook.Application\DependencyInjectionExtension.cs
	namespace 	
MyRecipeBook
 
. 
Application "
;" #
public		 
static		 
class		 (
DependencyInjectionExtension		 0
{

 
public 

static 
void 
AddApplication %
(% &
this& *
IServiceCollection+ =
services> F
,F G
IConfigurationI W
configurationX e
)e f
{ 
AddUseCases 
( 
services 
) 
; 
AddAutoMapper 
( 
services 
) 
;   
AddPasswordEncripter 
( 
services %
,% &
configuration( 5
)5 6
;6 7
} 
private 
static 
void 
AddAutoMapper %
(% &
this& *
IServiceCollection+ =
services> F
)F G
{ 
services 
. 
	AddScoped 
( 
option !
=>! #
new$ '

AutoMapper( 2
.2 3
MapperConfiguration3 F
(F G
optionsG N
=>N P
{ 	
options 
. 

AddProfile 
( 
new "
AutoMapping# .
(. /
)/ 0
)0 1
;1 2
} 	
)	 

.
 
CreateMapper 
( 
) 
) 
; 
} 
private 
static 
void 
AddUseCases #
(# $
this$ (
IServiceCollection) ;
services< D
)D E
{ 
services 
. 
	AddScoped 
<  
IRegisterUserUseCase /
,/ 0
RegisterUserUseCase2 E
>E F
(F G
)G H
;H I
} 
private 
static 
void  
AddPasswordEncripter ,
(, -
this- 1
IServiceCollection2 D
servicesE M
,M N
IConfigurationO ]
configuration^ k
)k l
{   
var!! 
additionalKey!! 
=!! 
configuration!! )
.!!) *
GetValue!!* 2
<!!2 3
string!!3 9
>!!9 :
(!!: ;
$str!!; \
)!!\ ]
;!!] ^
services"" 
."" 
	AddScoped"" 
("" 
option"" !
=>""" $
new""% (
PasswordEncripter"") :
("": ;
additionalKey""; H
!""H I
)""I J
)""J K
;""K L
}## 
}%% 