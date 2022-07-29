/*   
Template Name: Source Admin - Responsive Admin Dashboard Template build with Twitter Bootstrap 3.3.7 & Bootstrap 4
Version: 1.5.0
Author: Sean Ngu
Website: http://www.seantheme.com/source-admin-v1.5/admin/
*/var handleBootstrapWizardsValidation = function () {
    "use strict"; $("#wizard").bwizard({
        clickableSteps: false,
        backBtnText: "&larr; Anterior",
        nextBtnText: "Siguiente &rarr;",
        validating: function (e, a) {
            return (0 == a.index && a.nextIndex >= 0 || a.nextIndex > 0) && !1 === validateStep(1)
                ? !1 : (1 == a.index && a.nextIndex >= 1 || a.nextIndex > 1) && !1 === validateStep(2)
                    ? !1 : (2 == a.index && a.nextIndex >= 2 || a.nextIndex > 2) && !1 === validateStep(3, false)
                        ? !1 : (3 == a.index && a.nextIndex >= 3 || a.nextIndex > 3) && !1 === validateStep(4, false)
                            ? !1 : (4 == a.index && a.nextIndex >= 4 || a.nextIndex > 4) && !1 === validateStep(5, false)
                                ? !1 : (5 == a.index && a.nextIndex >= 5 || a.nextIndex > 5) && !1 === validateStep(6)
                                    ? !1 : void 0
        }
    })
}, PageDemo = function () {
    "use strict"; return {
        init: function () { handleBootstrapWizardsValidation() }
    }
}();