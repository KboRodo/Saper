PROJEKT Z PRZEMIOTU WPROWADZENIE DO PROGRAMOWANIA C#-GRA SAPER
AUTOR: Jakub Kościukiewicz


OPIS DZIAŁANIA PROGRAMU
Program pobiera od użytkownika od użytkownika dane dotyczące rozmiru planszy i ilości znajdujących się na nim min,
jedynie wpisanie odpowiednich liczb pozwala rozpocząć grę

Plansza jest zaimplementowana za pomocą klasy Minefield posiadającą tablicę dwuwymiarową obiektów klasy Element,
plansza przechowywuje informacje dotyczące ilości aktywnych i oznaczonych min oraz postawionych flag.
Powstaje plansze o podanych prametrach, pola w których znajdują się miny zostają wybrane w sposób losowy.

Menu wyboru opcji znajduje się wewnątrz pentli while po każdym jej przejściu gra sprawdza czy rozgrywka dobiegła końca
(czy liczba zdezaktywowanych min jest równa liczbie min powstałych przy tworzeniu planszy), jesli obie zmienne są sobie równe
następuje przerwanie pętli i wyswietlenie komunikatu o wygranej.

Użytkownik dostaje możliwość wyboru różnych opcji za pomocą wpisywania w terminal odpowiadających im liczb,
opcje te są następujące:
	-1-gra prosi o podanie pola w przypdaku gdy znajduję się na nim bomba zostaje przerwana pętla użytkownik dostaje komunikat o przegranej,
		w przeciwnym wypadku wywołana zostaje funkcja która "odkrywa" poszczególne elementy planszy dopóki nie natrafi
		na graniczące z bombą pole. 

	-2-gra prosi o podanie pola następnie na określonym polu umieszczona zostaje flaga, oraz jedna flaga zostaje odjęta z golbalnego limitu flag,
		jesli na polu znajduje sie mina, jej oznaczenie zostaje zapisane przez program.Po zakończeniu działania funkcji zostaje wyświetlona zaktalizowana wersja planszy
		
	-3-gra prosi o podanie pola następnie z określonego pola zostaje usunięta, oraz jedna flaga zostaje dodane do golbalnego limitu flag,
		jesli na polu znajduje sie mina, jej odznaczenie zostaje zapisane przez program.Po zakończeniu działania funkcji zostaje wyświetlona zaktalizowana wersja planszy

	-4-powoduje przerwanie pętli  wyjscie z gry

	-5 i 6 -opcje demonstracyjne służące do łatwiejszego zademonstrowania poprawności działania programu i jego demonstracji,
			opcja 5 powoduje wyswietlenie planszy z zaznaczonymi minami oraz polami z nimi graniczącymi a opcja 6 wywołuje
			funkcję wyświetlającą planszę gry w trybie normalnym.

Plansza wyświetlana jest za pomocą funkcji która sprawdza właściwości poszczególnych pól tabeli(czy zostały "odkryte", czy jest na nich flaga i czy graniczą z miną),
odpowienia informacja wyswietlana jest na ekranie:
"~" pole nie odkryte,\
" " pole odkryte ">"
"9" cyfra odpowiadająca ilości min graniczących z polem

Z wybranego przez urzytkownika punktu program szuka granic"pustego" obszaru przeszukując nieodkryte wcześniej pola znajdujące się w tej samej kolumnie
co wybrany przez uzytkownika punk, następnie z kazdego znajdującego się w kolumnie pole sprawdzane są znajdujące się w tym samym rzędzie nieokryte wcześniej pola.
Program konczy przeszukiwanie gdy natrafi na pole graniczące z bombą.





