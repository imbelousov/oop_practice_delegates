# SimpleChat

??? p2p ???, ???????????? ? ???????? ?????? ????? [UDP](https://ru.wikipedia.org/wiki/UDP). ? ??????? ??? ??????? ??????????????
?????????????? ????? ????????? ?? ?????? ??????? ??????, ?? ?? ?????????? ???????? ?????? ???????????.

# ??????? 1

???????? ????? `MessageEncoder` ???, ????? ?????? ??????????????, ? ??? ????????? ???????.
??????????? ????????????/?????????????? ?????????????! ?????????? ???? ?? ??? ?????? ???????
????????????, ????????, JSON ? ????????. ??????????? ??? ???, ????? ????????? ??????? ????????????
?? ????????? ???????? ????????? ? ???????? ??? ?????? `MessageEncoder`.

# ??????? 2

??? ??????? ????????, ?? ???????????? ????? ???? ?????????? ? ?????????????????? ?? ???????.
????? ????, ????????? ?????? ?? ???????? ?????? ?? ????? ? ????????????????? ??????????.
??????????? ????? `MessageEncoder` ???, ????? ?? ???? ??????????? ? ?????????? ??? ??? ??????????:
?????????? ? ?????? ?????????. ?????????? ?????? ???????? ??? ?? ???????????, ??? ? ??????.
??????????? ??? ???, ????? ?????????? ?????????? ?? ????????? ???????? ?????????  ? ???????? ???
?????? `MessageEncoder`. ????? ???????????? ????? ????????? ?????????? ? ?????? ?? ??? ?????.
????????, [AES](https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.aes?view=net-5.0) ? [Deflate](https://docs.microsoft.com/en-us/dotnet/api/system.io.compression.deflatestream?view=net-5.0).

# ??????? 3

????????? ????????????? ???????? ????? ????????? ? ??????????? ???????? ????? ???????.
??????????, ???????? ??????? ????????? ?????? ???????????. ????? ????, ?????? ? ??????????
?????? ???????? ? ??? ??????. ??? ???????? ?????? ??????????? ????? `BinaryMessage` ?????? `TextMessage`.
?? ???????, ???? ?? ?? ??????? ?????????? ????????? ???? - ???????, ????? ????????? ??????????? ???????????
???????? ?????? ????? ????? ?????.