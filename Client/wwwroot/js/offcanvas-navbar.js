(() => {
  'use strict'

    if (document.querySelector('#navbarSideCollapse') !== null) {
        document.querySelector('#navbarSideCollapse').addEventListener('click', () => {
            document.querySelector('.offcanvas-collapse').classList.toggle('open')
        })
    }
})()
