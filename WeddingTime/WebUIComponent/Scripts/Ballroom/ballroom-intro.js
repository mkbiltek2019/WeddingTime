'use strict'
var ballroomIntro = function () {

    var init = function () {
        if ($.browser.mobile)
            return;

        var value = cookieManager.get('ballroominfo');
        if (value !== null && value.introClosed === true)
            return;
  
        initLayout();

        var intro = introJs();
        initIntroJs(intro);
        initSteps(intro);

        intro.oncomplete(function () {
            cleanup();
            setCookie();                
        }).onexit(function () {
            cleanup();
            setCookie();
        });
    },

    initLayout = function () {
        renderTableCollapsed();
        renderTableExpanded();
        $('.guests-panel').show();
        $('#btnToggleLayout').addClass('disabled');
        $('#btnPersonsPanel').addClass('disabled');
        $('#btnBallroomItems').addClass('disabled');       
        $('#expandLayoutBtns').find('button').addClass('disabled');
    },

    setCookie = function () {
        cookieManager.set('ballroominfo', { introClosed: true }, '/Ballroom');
    },

    cleanup = function () {
        $('.guests-panel').hide();
        $('#a').remove();
        $('#b').remove();
        $('#btnToggleLayout').removeClass('disabled');
        $('#btnPersonsPanel').removeClass('disabled');
        $('#btnBallroomItems').removeClass('disabled');
        $('#expandLayoutBtns').find('button').removeClass('disabled');
    },

    renderTableCollapsed = function () {
        var item = renderTable('a');
        item.css({ 'top': '90px', 'left': '360px' });
    },

    renderTableExpanded = function () {
        var item = renderTable('b');
        item.find('.btn-show-items').hide();
        item.find('.item-panel-content').show();
        item.css({ 'top': '40px', 'left': '150px' });
    },

    renderTable = function (id) {
        var tmplData = {
            ItemTmpl: 'tableRect',
            PanelTmpl: 'tableRectPanelBtns',
            Data: getFakeData(id)
        };

        $('#ballroomLayout').append($.render['ballroomItem'](tmplData));    // render template that was register on page load
        var item = $('#' + id);        
        item.find('.table-rect-area').css({ 'width': '150px', 'height': '112px' });        
        $('#item-' + id).css('left', '15px');

        return item;
    },

    getFakeData = function (id) {
        return JSON.parse('{"Id":"' + id + '","PositionX":0,"PositionY":0,"Rotation":0,"ItemType":0,"Seats":{"1":{"PersonId":null,"TakenBy":null,"TableId":0,"Hidden":false,"Occupied":false},"2":{"PersonId":null,"TakenBy":null,"TableId":0,"Hidden":false,"Occupied":false},"3":{"PersonId":null,"TakenBy":null,"TableId":0,"Hidden":false,"Occupied":false,"Location":0},"4":{"PersonId":null,"TakenBy":null,"TableId":0,"Hidden":false,"Occupied":false,"Location":1}},"TopSeat":{"Key":3,"Value":{"PersonId":null,"TakenBy":null,"TableId":0,"Hidden":false,"Occupied":false,"Location":0}},"BottomSeat":{"Key":4,"Value":{"PersonId":null,"TakenBy":null,"TableId":0,"Hidden":false,"Occupied":false,"Location":1}},"SideSeats":{"1":{"PersonId":null,"TakenBy":null,"TableId":0,"Hidden":false,"Occupied":false},"2":{"PersonId":null,"TakenBy":null,"TableId":0,"Hidden":false,"Occupied":false}}}');
    },

    initIntroJs = function (intro) {
        intro.setOptions({
            'nextLabel': 'Dalej',
            'skipLabel': 'Zakończ',
            'prevLabel': 'Wstecz',
            'doneLabel': 'Zakończ',
            'exitOnEsc': false,
            'exitOnOverlayClick': false
        });
    },

    initSteps = function (intro) {
        intro.setOptions({
            steps: [
              {
                  intro: 'Zapoznaj się z funkcjami strony.'
              },
              {
                  element: '#btnBallroomItems',
                  intro: 'Wyświetl elementy i nanieś je na plan sali weselnej.'
              },
              {
                  element: $('#a').find('.btn-show-items')[0],
                  intro: 'Rozwiń panel akcji dla danego elemntu sali weselnej.',
                  position: 'top'
              },
              {
                  element: $('#b').find('.item-panel-content')[0],
                  intro: 'Zamknij panel / usuń element / dodaj krzesła / obróc element / przesuń element.',
                  position: 'top'
              },
              {
                  element: $('#b').find('div[sid=4]')[0],
                  intro: 'Kliknij w symbol krzesła w celu usunięcia przypisanej do niego osoby lub ukrycia / usunięcia krzesła.',
                  position: 'bottom'
              },              
              {
                  element: '#btnPersonsPanel',
                  intro: 'Wyświetl listę gości.',
                  position: 'left'
              },
              {
                  element: $('li[pid=groom]').find('.list-item-handle')[0],
                  intro: 'Przyporządkuj gości do krzeseł używając tego uchwytu.',
                  position: 'left'
              },
              {
                  element: '#expandLayoutBtns',
                  intro: 'Powiększ / pomniejsz plan sali weselnej.',
                  position: 'left'
              },              
              {
                  element: '#btnSaveLayout',
                  intro: 'Zapisz wprowadzone zmiany!'
              },                           
              {
                  intro: 'To już wszystko. Życzymy miłego planowania Twojej sali weselnej.'
              }
            ]
        });

        if ($('.sub-img').parent().css('display') !== 'none') {             // insert step only for md and lg
            intro._options.steps.splice(8, 0, {
                element: '#btnToggleLayout',
                intro: 'Wyświetl plan sali na większym ekranie.',
                position: 'right'
            });
        }

        intro.start();
    };

    return {
        init: init
    };
}();