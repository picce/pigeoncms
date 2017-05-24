<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageComposer.ascx.cs" Inherits="Controls_PageComposer_PageComposer" %>

<asp:Panel ID="pnlNewsComposer" runat="server">
    <div class="form-group col-md-12">
        <div id="aq-composer-wrapper">
            <div class="composer-content col-md-9">
                <div class="composer-title">
                    Trascina qui un template per aggiungere un blocco alla pagina 
                                                   
                </div>
            </div>
            <div class="composer-toolbar col-md-3">
                <div class="composer-title">
                    Lingua
                                                   
                </div>
                <div class="field-wrapper">
                    <select id="cmbLanguages" class="composer-field">
                        <option value="it-IT" selected>Italiano</option>
                        <option value="en-US">Inglese</option>
                    </select>
                </div>
                <div class="composer-title">
                    Templates disponibili
                                                   
                </div>
                <div class="composer-help">
                    (trascina per aggiungere un elemento)           
                                                   
                </div>

                <div class="block-item block-template grid-100" data-pagecomposerrender="Header">
                    <div class="block-task u-text--right">
                        <span class="block-task-icon task-move"></span>
                    </div>
                    <div class="u-text--center">
                        Testata<br />
                        (immagine con titolo)
               
                                                       
                    </div>
                </div>

                <div class="block-item block-template grid-100" data-pagecomposerrender="ImageText">
                    <div class="block-task u-text--right">
                        <span class="block-task-icon task-move"></span>
                    </div>
                    <div class="u-text--center">
                        Testo con immagine laterale
                                                       
                    </div>
                </div>

                <div class="block-item block-template grid-100" data-pagecomposerrender="Image">
                    <div class="block-task u-text--right">
                        <span class="block-task-icon task-move"></span>
                    </div>
                    <div class="u-text--center">
                        Immagine con testo
                                                       
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="aq_pagecomposer_value" runat="server" Visible="true" ClientIDMode="Static" />
        </div>
    </div>
</asp:Panel>
