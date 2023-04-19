import { Grad } from "./Grad.js";

export class Prilika{

    constructor(id, mesec, temperatura, padavine, suncaniDani){
        this.id = id;
        this.mesec = mesec;
        this.temperatura = temperatura;
        this.padavine = padavine;
        this.suncaniDani = suncaniDani;
        this.kontejner = null;
    }

    iscrtajPriliku(host, vrednost, mesecIme){

        this.kontejner = document.createElement("div");
        this.kontejner.className = "skalaKolona";
        let skaleDiv = document.querySelector(".skaleDiv");

        let prilika;
        let prilikaOzn;
        let procenat;
        let max;

        if(vrednost == 1)
        {
            max = 50;
            prilika = this.temperatura;
            procenat = (this.temperatura/max)*100;
            prilikaOzn="°C";

        }
        else if(vrednost == 2)
        {
            max = 1000;
            prilika = this.padavine;
            procenat = (this.padavine/max)*100;
            prilikaOzn = "mm";
        }
        else
        {
            max = 31;
            prilika = this.suncaniDani;
            procenat = (this.suncaniDani/max)*100;
            prilikaOzn = "";
        }

        let skalalbl = document.createElement("label");
        skalalbl.className = "mesecIme";
        skalalbl.innerHTML = mesecIme;
        this.kontejner.appendChild(skalalbl);

        let skalaKont = document.createElement("div");
        skalaKont.className = "skalaKont";
        this.kontejner.appendChild(skalaKont);

        let skalaVrednost = document.createElement("div");
        skalaVrednost.className = "skalaIzgled";
        skalaVrednost.style.height= `${procenat}%`;
        skalaKont.appendChild(skalaVrednost);

        let vrednostLbl = document.createElement("label");
        vrednostLbl.className = "vrednost";
        vrednostLbl.innerHTML = prilika+""+prilikaOzn;
        this.kontejner.appendChild(vrednostLbl);

        host.appendChild(this.kontejner);

        this.kontejner.onclick = (ev) => this.popUpUnos(skaleDiv, this, mesecIme, vrednost);

    }

    popUpUnos(host, prilika, mesec, rbtnV){

        let popDiv = document.createElement("div");
        popDiv.className = "popDiv";

        let popLbl = document.createElement("label");
        popLbl.className = "popLbl";

        let mesecLbl = mesec;
        popLbl.innerHTML = "Mesec: "+mesecLbl;
        popDiv.appendChild(popLbl);

        let tbx = document.createElement("input");
        tbx.type = "number";
        tbx.className = "textBox";
        popDiv.appendChild(tbx);

        let btn = document.createElement("button");
        btn.innerHTML = "Upisi";
        popDiv.appendChild(btn);
        btn.onclcik = (ev) => prilika.upisi(rbtnV, tbx);

        host.appendChild(popDiv);

    }

    upisi(rbtnV, tbx){

        let nova = tbx.value;
        console.log(nova);

        if(nova === ""){
            alert("Morate uneti novu brojcanu vrednost!");
            return;
        }

        let div = this.Grad.querySelector(".skalePrikaz");

        if(rbtnV == 1){

            fetch("http://localhost:5286/MeteoroloskaPrilika/PromeniTemperaturu/"+this.id+"/"+nova, {
                method: "PUT"
            }).then(data => {
                data.json().then(info => {
                    console.log(info);
                    this.temperatura = info.temperatura;
                    this.Grad.iscrtajSkale(div);

                    // let max = 50;
                    // let skalaZaPromenu = document.querySelector(".skalaIzgled");
                    // let procenat = (this.temperatura/max)*100;
                    // skalaZaPromenu.style.height = `${procenat}%`;
                });
            });


        }
        else if(rbtnV == 2){

            fetch("http://localhost:5286/MeteoroloskaPrilika/PromeniPadavine/"+this.id+"/"+nova, {
                method: "PUT"
            }).then(data => {
                data.json().then(info => {
                    console.log(info);
                    this.padavine = info.padavine;
                    this.Grad.iscrtajSkale(div);

                    // let max = 1000;
                    // let skalaZaPromenu = document.querySelector(".skalaIzgled");
                    // let procenat = (this.padavine/max)*100;
                    // skalaZaPromenu.style.height = `${procenat}%`;
                });
            });

        }
        else{

            fetch("http://localhost:5286/MeteoroloskaPrilika/PromeniSuncaneDane/"+this.id+"/"+nova, {
                method: "PUT"
            }).then(data => {
                data.json().then(info => {
                    console.log(info);
                    this.suncaniDani = info.suncaniDani;
                    this.Grad.iscrtajSkale(div);
                    // let max = 31;
                    // let skalaZaPromenu = document.querySelector(".skalaIzgled");
                    // let procenat = (this.suncaniDani/max)*100;
                    // skalaZaPromenu.style.height = `${procenat}%`;
                });
            });

        }
    }

}