
// Sliders 
var count = 1;
setInterval(function(){
    document.getElementById('radio'+ count).checked= true;
    count ++;
    if(count > 3){
        count = 1;
    }
}, 4000)


// Sliders details

var imgs = document.getElementsByClassName('imgs');

var activeImg = document.getElementsByClassName('active');

for (var i = 0; i < imgs.length; i++) {
    imgs[i].addEventListener('mouseover', function () {

        if (activeImg.length > 0) {
            activeImg[0].classList.remove('active')
        }

        this.classList.add('active')
        document.getElementById('img__big').src = this.src
    });
}


// Tabs detail

var $ = document.querySelector.bind(document)
var $$ = document.querySelectorAll.bind(document)

var tabs = $$('.info-item')
var panes = $$('.tab-pane')

tabs.forEach((tab, index) => {
    var pane = panes[index]

    tab.onclick = function () {
        $('.info-item.active').classList.remove('active')
        $('.tab-pane.active').classList.remove('active')

        this.classList.add('active')
        pane.classList.add('active')
    }
});