import sys

from PyQt5 import QtWidgets
from PyQt5.QtWidgets import QApplication

from db_connector import DBConnector
from register_form import RegisterForm


def main():
    controller = Controller()
    controller.show_form()

class Controller:
    def __init__(self):
        self.app = QApplication(sys.argv)
        self.db_connector = DBConnector()
        self.register_form = RegisterForm(self.db_connector)

    def show_form(self):
        self.register_form.show()
        sys.excepthook = lambda exctype, value, traceback: self.show_error_message(str(value), traceback)
        sys.exit(self.app.exec_())

    @staticmethod
    def show_error_message(message, traceback):
        msg_box = QtWidgets.QMessageBox()
        msg_box.setIcon(QtWidgets.QMessageBox.Critical)
        msg_box.setWindowTitle("Error")
        msg_box.setText(message)
        msg_box.exec_()
        traceback.print_exc()


if __name__ == '__main__':
    main()
