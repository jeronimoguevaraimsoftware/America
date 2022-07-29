function loadSavedDataOA() {
    // Table 1
    $("#table-1").find("tbody tr").each(function (index) {
        let checked = $(this).find("#AnalyticEquipmentIsCalibrated" + index).val()

        if (checked === "True") {
            $(this).find("#AnalyticEquipmentCheckTrue" + index).prop("checked", true)
            $(this).find("#AnalyticEquipmentCheckFalse" + index).prop("checked", false)
        }
        else if (checked === "False") {
            $(this).find("#AnalyticEquipmentCheckTrue" + index).prop("checked", false)
            $(this).find("#AnalyticEquipmentCheckFalse" + index).prop("checked", true)
        }

        let reviewedBy = $(this).find("#AnalyticEquipmentReviewedBy" + index).val()
        let reviewedDateString = $(this).find("#AnalyticEquipmentReviewedDate" + index).val()
        let reviewedDate = !isNullOrEmpty(reviewedDateString) ? new Date(reviewedDateString) : null
        $(this).find("#AnalyticEquipmentReviewedSignature" + index).html(getSignature(reviewedBy, reviewedDate))
    });

    // Table 2
    $("#table-2").find("tbody tr").each(function (index) {
        let checked = $(this).find("#ScalesflowsIsCalibrated" + index).val()

        if (checked === "True") {
            $(this).find("#ScalesflowsCheckTrue" + index).prop("checked", true)
            $(this).find("#ScalesflowsCheckFalse" + index).prop("checked", false)
        }
        else if (checked === "False") {
            $(this).find("#ScalesflowsCheckTrue" + index).prop("checked", false)
            $(this).find("#ScalesflowsCheckFalse" + index).prop("checked", true)
        }

        let reviewedBy = $(this).find("#ScalesflowsReviewedBy" + index).val()
        let reviewedDateString = $(this).find("#ScalesflowsReviewedDate" + index).val()
        let reviewedDate = !isNullOrEmpty(reviewedDateString) ? new Date(reviewedDateString) : null
        $(this).find("#ScalesflowsReviewedSignature" + index).html(getSignature(reviewedBy, reviewedDate))
    });

    // Table 3
    let checkedPipelineClearanceInCompliance = $("#PipelineClearanceInCompliance").val()
    if (checkedPipelineClearanceInCompliance === "True") {
        $("#PipelineClearanceCheckTrue").prop("checked", true)
        $("#PipelineClearanceCheckFalse").prop("checked", false)
    }
    else if (checkedPipelineClearanceInCompliance === "False") {
        $("#PipelineClearanceCheckTrue").prop("checked", false)
        $("#PipelineClearanceCheckFalse").prop("checked", true)
    }

    let reviewedBy = $("#PipelineClearanceReviewedBy").val()
    let reviewedDateString = $("#PipelineClearanceReviewedDate").val()
    let reviewedDate = !isNullOrEmpty(reviewedDateString) ? new Date(reviewedDateString) : null
    $('#PipelineClearanceReviewedSignature').html(getSignature(reviewedBy, reviewedDate))

    // Table 4
    loadSavedDataTable4()

    // Table 5
    loadSavedDataTable5()

    // Table 6
    $("#table-6").find("tbody tr").each(function (index) {
        let reviewedBy = $(this).find("#PerformanceListReviewedBy" + index).val()
        let reviewedDateString = $(this).find("#PerformanceListReviewedDate" + index).val()
        let reviewedDate = !isNullOrEmpty(reviewedDateString) ? new Date(reviewedDateString) : null
        $(this).find("#PerformanceListReviewedSignature" + index).html(getSignature(reviewedBy, reviewedDate))
    });

    // Table 7
    let releasedBy = $("#ReleasedBy").val()
    let releasedDateString = $("#ReleasedDate").val()
    let releasedDate = !isNullOrEmpty(releasedDateString) ? new Date(releasedDateString) : null
    $('#ReleasedSignature').html(getSignature(releasedBy, releasedDate))

    $('#step1').find('button').each(function () {
        $(this).prop("disabled", true);
    });
    $('#step2').find('button').each(function () {
        $(this).prop("disabled", true);
    });
    $('#step3').find('button').each(function () {
        $(this).prop("disabled", true);
    });
    $('#step4').find('button').each(function () {
        $(this).prop("disabled", true);
    });
    $('#step5').find('button').each(function () {
        $(this).prop("disabled", true);
    });
    $('#step6').find('button').each(function () {
        $(this).prop("disabled", true);
    });

    $('#step1').find('input').each(function () {
        $(this).prop("disabled", true);
    });
    $('#step2').find('input').each(function () {
        $(this).prop("disabled", true);
    });
    $('#step3').find('input').each(function () {
        $(this).prop("disabled", true);
    });
    $('#step4').find('input').each(function () {
        $(this).prop("disabled", true);
    });
    $('#step5').find('input').each(function () {
        $(this).prop("disabled", true);
    });
    $('#step6').find('input').each(function () {
        $(this).prop("disabled", true);
    });
    enabledFiles();
}

function loadSavedDataTable4() {
    $("#table-4").find("tbody tr").each(function (index) {
        $(this).find(".sign-equipment-process").remove()

        let reviewedBy = $(this).find("#EquipamentProcessesReviewedBy" + index).val()
        let reviewedDateString = $(this).find("#EquipamentProcessesReviewedDate" + index).val()
        let reviewedDate = !isNullOrEmpty(reviewedDateString) ? new Date(reviewedDateString) : null
        $(this).find("#EquipamentProcessesReviewedSignature" + index).html(getSignature(reviewedBy, reviewedDate))
    });

    $("#table-4").find('.selectpicker').each(function () {
        var element = document.createElement('input')
        element.value = $(this).val()
        element.disable = true

        this.parentNode.replaceChild(element, this)
    })
}

function loadSavedDataTable5() {
    $("#pipeFilling-container .tab-content .tab-pane").each(function () {
        let controlType = $(this).attr("data-type")
        let tournumberIndex = $(this).attr("data-index")

        if (controlType === 'tournumber') {
            $(this).find(".tab-content .tab-pane").each(function () {
                let controlType = $(this).attr("data-type")
                let pipeIndex = $(this).attr("data-index")

                $(this).find("#btnPipeFillingWeb-" + tournumberIndex + '-' + pipeIndex).remove()
                $(this).find("#btnPipeFillingManual-" + tournumberIndex + '-' + pipeIndex).remove()
                $(this).find("#checkListManualCtrls-" + tournumberIndex + '-' + pipeIndex).remove()

                let checkedCheckListInComplianceCheck = $(this).find("#CheckListIncompliance-" + tournumberIndex + '-' + pipeIndex).val()
                if (checkedCheckListInComplianceCheck === "True") {
                    $(this).find("#CheckListIncomplianceCheckTrue-" + tournumberIndex + '-' + pipeIndex).prop("checked", true)
                    $(this).find("#CheckListIncomplianceCheckFalse-" + tournumberIndex + '-' + pipeIndex).prop("checked", false)
                }
                else if (checkedCheckListInComplianceCheck === "False") {
                    $(this).find("#CheckListIncomplianceCheckTrue-" + tournumberIndex + '-' + pipeIndex).prop("checked", false)
                    $(this).find("#CheckListIncomplianceCheckFalse-" + tournumberIndex + '-' + pipeIndex).prop("checked", true)
                }
                let pipeFillingInComplianceCheck = $(this).find("#PipeFillingInCompliance-" + tournumberIndex + '-' + pipeIndex).val()
                if (pipeFillingInComplianceCheck === "True") {
                    $(this).find("#PipeFillingInComplianceCheckTrue-" + tournumberIndex + '-' + pipeIndex).prop("checked", true)
                    $(this).find("#PipeFillingInComplianceCheckFalse-" + tournumberIndex + '-' + pipeIndex).prop("checked", false)
                }
                else if (pipeFillingInComplianceCheck === "False") {
                    $(this).find("#PipeFillingInComplianceCheckTrue-" + tournumberIndex + '-' + pipeIndex).prop("checked", false)
                    $(this).find("#PipeFillingInComplianceCheckFalse-" + tournumberIndex + '-' + pipeIndex).prop("checked", true)
                }
                let pipeFillingFinalAnalysiIsReleasedsCheck = $(this).find("#PipeFillingIsReleased-" + tournumberIndex + '-' + pipeIndex).val()
                if (pipeFillingFinalAnalysiIsReleasedsCheck === "True") {
                    $(this).find("#PipeFillingReleasedCheckTrue-" + tournumberIndex + '-' + pipeIndex).prop("checked", true)
                    $(this).find("#PipeFillingReleasedCheckFalse-" + tournumberIndex + '-' + pipeIndex).prop("checked", false)
                }
                else if (pipeFillingFinalAnalysiIsReleasedsCheck === "False") {
                    $(this).find("#PipeFillingReleasedCheckTrue-" + tournumberIndex + '-' + pipeIndex).prop("checked", false)
                    $(this).find("#PipeFillingReleasedCheckFalse-" + tournumberIndex + '-' + pipeIndex).prop("checked", true)
                }
                let pipeFillingAnalyzedBy = $(this).find("#PipeFillingAnalyzedBy-" + tournumberIndex + '-' + pipeIndex).val()
                let pipeFillingAnalyzedDateString = $(this).find("#PipeFillingAnalyzedDate-" + tournumberIndex + '-' + pipeIndex).val()
                let pipeFillingAnalyzedDate = !isNullOrEmpty(pipeFillingAnalyzedDateString) ? new Date(pipeFillingAnalyzedDateString) : null
                $('#PipeFillingAnalyzedSignature-' + tournumberIndex + '-' + pipeIndex).html(getSignature(pipeFillingAnalyzedBy, pipeFillingAnalyzedDate))
                let pipeFillingReleasedBy = $(this).find("#PipeFillingReleasedBy-" + tournumberIndex + '-' + pipeIndex).val()
                let pipeFillingReleasedDateString = $(this).find("#PipeFillingReleasedDate-" + tournumberIndex + '-' + pipeIndex).val()
                let pipeFillingReleasedDate = !isNullOrEmpty(pipeFillingReleasedDateString) ? new Date(pipeFillingReleasedDateString) : null
                $('#PipeFillingReleasedSignature-' + tournumberIndex + '-' + pipeIndex).html(getSignature(pipeFillingReleasedBy, pipeFillingReleasedDate))

                $("#table-5-customers-" + tournumberIndex + '-' + pipeIndex).find("tbody tr").each(function (index) {
                    $(this).find(".sign-pipe-customer").remove()

                    let reviewedBy = $(this).find("#PipeFillingCustomerReviewedBy" + index + '-' + tournumberIndex + '-' + pipeIndex).val()
                    let reviewedDateString = $(this).find("#PipeFillingCustomerReviewedDate" + index + '-' + tournumberIndex + '-' + pipeIndex).val()
                    let reviewedDate = !isNullOrEmpty(reviewedDateString) ? new Date(reviewedDateString) : null
                    $(this).find("#PipeFillingCustomerReviewedSignature" + index + '-' + tournumberIndex + '-' + pipeIndex).html(getSignature(reviewedBy, reviewedDate))
                    
                    if (isNullOrEmpty($(this).find("#PipeFillingCustomerReviewedDate" + index + '-' + tournumberIndex + '-' + pipeIndex).val())) {
                        $(this).find("#spanSignature" + index + '-' + tournumberIndex + '-' + pipeIndex).removeAttr("hidden");
                    }
                    if (isNullOrEmpty($(this).find("#PipeFillingCustomerAnalysisReport" + index + '-' + tournumberIndex + '-' + pipeIndex).val())) {
                        $(this).find("#spanReport" + index + '-' + tournumberIndex + '-' + pipeIndex).removeAttr("hidden");
                    }
                });
                $(`#btnPipeFillingShowHistory-${tournumberIndex}-${pipeIndex}`).click();
                $(`#btnPipeFillingShowHistory-${tournumberIndex}-${pipeIndex}`).prop("disabled", true);
                $(`#btnUpdateCheckList-${tournumberIndex}-${pipeIndex}`).prop("disabled", true);
            })
        }
    })

    $("#pipeFilling-tourNumber-tab-0").tab('show')
    $("#pipeFilling-pipe-tab-0-0").tab('show')
}

var showPipelineClearanceHistoryOA = function (event) {
    //Despeje de línea historial
    $.ajax({
        type: "GET",
        url: "/ConditioningOrder/GetDetailDL/?Id=" + $("#Id").val(),
        success: function (partialViewResult) {
            $("#table3-detailInfo").html(partialViewResult);
            $('#table3-detailInfo').prop('hidden', false);

            // load signature and second notes
            $("#table-3Detail").find("tbody tr").each(function (index) {
                let reviewedBy = $(this).find("#DeviationReportReviewedBySecond" + index).val()
                let reviewedDateString = $(this).find("#DeviationReportReviewedDateSecond" + index).val()
                let reviewedDate = !isNullOrEmpty(reviewedDateString) ? new Date(reviewedDateString) : null
                $(this).find("#DeviationReportReviewedSecondSignature" + index).html(getSignature(reviewedBy, reviewedDate))
            });

            $("#table-3Detail").find(".sign-deviation").each(function (index) {
                $(this).remove()
            })
        }
    });
}

var hidePipelineClearanceHistoryOA = function (event) {
    $('#table3-detailInfo').prop('hidden', true);
}


function pipeFillingRead(idOA, tourNumber, pipe, distributionBatch, checkListId) {
    window.open(window.location.origin + '/CheckListQuestions/QuestionsTwo?idOA=' + idOA + '&tourNumber=' + tourNumber + '&distributionBatch=' + distributionBatch + '&checkListId=' + checkListId, '_blank');
}

function enabledFiles() {
    //AHF
    const filesInitial = $('[id^=PipeFillingInitialAnalysisPath]');
    if (!!filesInitial && !!filesInitial.length) {
        $.each(filesInitial, (index, x) => {
            if (!!x.value) {
                const id = x.id.replace("PipeFillingInitialAnalysisPath", "");
                $(`#btnuploadInitialFile${id}`).hide();
                $(`#btnshowInitialFile${id}`).show();
                $(`#btnshowInitialFile${id}`).prop("disabled", false);
            }
        });
    }
    const filesFinal = $('[id^=PipeFillingFinalAnalysisPath]');
    if (!!filesFinal && !!filesFinal.length) {
        $.each(filesFinal, (index, x) => {
            if (!!x.value) {
                const id = x.id.replace("PipeFillingFinalAnalysisPath", "");
                $(`#btnuploadFinalFile${id}`).hide();
                $(`#btnshowFinalFile${id}`).show().prop("disabled", false);
            }
        });
    }
}
function showFile(analisis, index) {
    if (!!$(`#PipeFilling${analisis}AnalysisAtributoValue${index}`).val()) {
        $(`#PipeFilling${analisis}AnalysisAtributoLink${index}`)[0].click();
    } else {
        $(`#PipeFilling${analisis}ShowLink${index}`)[0].click();
    }
}