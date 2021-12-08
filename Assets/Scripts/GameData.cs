using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameData : MonoBehaviour{
    private WrapperData data;
    public Text textResult;
    private string result;

    private void Start(){
        data = GetComponent<LoadData>().ReadJson();
    }

    public void Question01(){
        //Listar em ordem descendente, os 3 jogadores com maior número de pontos.
        data = GetComponent<LoadData>().ReadJson();
        List<Players> playerPoint = data.players.OrderByDescending(players => players.points).Take(3).ToList();

        foreach(Players players in playerPoint){
            textResult.text += (players.name + " - " + players.points + "\n");
        }
    }
    public void Question02(){
        //Ordenar por país os jogadores que ainda não criaram heróis.
        data = GetComponent<LoadData>().ReadJson();
        List<Players> playerCountry = data.players.Where(p => p.heroes.Count == 0).OrderBy(c => c.countryName).ToList();

        foreach(Players players in playerCountry){
            textResult.text += (players.countryName + " - " + players.name + "\n");
        }
    }
    public void Question03(){
        //Qual é a classe de herói mais criada e a menos criada.
        data = GetComponent<LoadData>().ReadJson();
        
        var res =
            from c in data.players.SelectMany(players => players.heroes)
            group c by c.heroClassName
            into g
            orderby g.Count() descending 
            select new {ClassHero = g.Key, count = g.Count()};

        //foreach(var c in res){
        //    textResult.text += (c.ClassName + c.count + "\n");
        //}

        textResult.text = ("Classe de herói mais criada: " + res.First().ClassHero + "\n" + "Classe de herói menos criada: " + res.Last().ClassHero);
    }

    public void Question04(){
        //Qual é o país que possui mais jogadores.
        data = GetComponent<LoadData>().ReadJson();
        
        var res =
            from c in data.players.Select(players => players.countryName)
            group c by c
            into g
            orderby g.Count() descending 
            select new {ClassCountry = g.Key/*,count = g.Count()*/};

        //foreach(var c in res){
        //    textResult.text += (c.Country + c.count+ "\n");
        //}

        textResult.text = (res.First().ClassCountry);
    }
    
    public void Question05(){
        //Qual plataforma possui os jogadores com melhores pontos.
        data = GetComponent<LoadData>().ReadJson();

        var res = data.players.GroupBy(player => new {player.platformName}).Select(group => new {Average = group.Average(player => player.points),PlatformName = group.Key.platformName}).OrderByDescending(order => order.Average);

        textResult.text = (res.First().PlatformName);
    }
    public void Question06(){
        //Listar os 10 jogadores com maior total de "gold".
        data = GetComponent<LoadData>().ReadJson();
        //int qtde = 0;
        for(int i = 0; i <= 10; i++){
            var res = data.players[i].heroes.GroupBy(player => new {player.name}).Select(group => new {Sum = group.Sum(heroes => heroes.gold),Name = group.Key.name}).OrderByDescending(order => order.Sum).Take(10);
            foreach(var c in res){
                textResult.text += (res + "\n");
            }
        }
        //foreach(Heroes players in res){
        //    textResult.text += (players.name + " - " + players.points + "\n");
        //}
    }

    public void Clear(){
        textResult.text = "";
    }
}