# Tarkvaraarendus - raamatukogu rakendus
**RamboRaamatud™**

## Funktsioonid
- raamatute vaatamine
  + raamatute haldamine (lisamine(isbn põhjal), eemaldamine ja kui jõuab siis andmete muutmine)
- kasutaja vaade
  + kasutajate haldamine (lisamine, eemaldamine ja kui jõuan siis andmete muutmine) 
-  Laenutus
  + Laenutamine ja tagastamine

## Andmebaas
- raamatute tabel:
  + väljaanne (isbn - vb saab kusagilt apist selle põhjal muid andmeid tõmmata, oleks kasulik raamatute lisamisel)
  + nimi
  + autor
  + väljaande aasta
  + id (kindlale füüsilisele raamatule unikaalne) - see ühendaks muude tabelitega
  + staatus (kohal/laenutatud)
  + tagastamiskuupäev (puudub/kp)
- kasutajate tabel:
  + nimi
  + isikukood
  + koduaadress
  + laenutatud raamatud
  + laenutatud raamatute tähtajad
- neid ühendab nt. laenutatud raamatu id
