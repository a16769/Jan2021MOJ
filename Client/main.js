import { Grad } from "./Grad.js"
import { Prilika } from "./Prilika.js"

let Gradovi =[];

function iscrtajGradove(){
    Gradovi.forEach(i => {
        i.iscrtajGrad();
    });
}


fetch("http://localhost:5286/Grad/PreuzmiGradove", {
    method: "GET"
}).then(data => {
    data.json().then(info => {
        info.forEach(g => {
            console.log(g);
            let grad = new Grad(g.id, g.naziv, g.longitude, g.latitude);
            g.prilike.forEach(p => {
                let prilika = new Prilika(p.id, p.mesec, p.temperatura, p.padavine, p.suncaniDani);
                prilika.Grad = grad;
                grad.Prilike.push(prilika);
            });
            Gradovi.push(grad);
        })
        iscrtajGradove();
    });
});