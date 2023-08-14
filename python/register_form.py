from register_user_form_compiled import Ui_RegisterForm
from PyQt5 import QtCore, QtGui, QtWidgets


class RegisterForm(QtWidgets.QWidget, Ui_RegisterForm):
    def __init__(self, db_conn):
        super().__init__()
        self.setupUi(self)
        self.db_conn = db_conn


